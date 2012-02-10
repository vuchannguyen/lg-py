<?php

/**
 *	Filemanager PHP class
 *
 *	filemanager.class.php
 *	class for the filemanager.php connector
 *
 *	@license	MIT License
 *	@author		Riaan Los <mail (at) riaanlos (dot) nl>
 *	@copyright	Authors
 */

class Filemanager {
	
	protected $config = array();
	protected $get = array();
	protected $post = array();
	protected $properties = array();
	protected $item = array();
	protected $root = '';
	protected $uploadpath;
	protected $uploadurl;
	protected $params;
	
	public function __construct($config) {
		$this->config = $config;
		$this->root = str_replace('connectors/php/filemanager.php','',$_SERVER['SCRIPT_FILENAME']);
		$this->properties = array(
			'Date Created'=>null, 
			'Date Modified'=>null, 
			'Height'=>null,
			'Width'=>null,
			'Size'=>null
		);
		
		$this->uploadpath = $config['root'];
		$this->uploadurl = $config['rooturl'];
		
		$this->setParams();
	}
	
	public function error($string,$textarea=false) {
		$array = array(
			'Error'=>$string,
			'Code'=>'-1',
			'Properties'=>$this->properties
		);
		if($textarea) {
			echo '<textarea>' . json_encode($array) . '</textarea>';
		} else {
			echo json_encode($array);
		}
		die();
	}
	
	public function lang($string) {
		require('lang/en.php');
		$language = strtolower(substr($_SERVER['HTTP_ACCEPT_LANGUAGE'],0,2));
		if(file_exists('lang/' . $language . '.php')) {
			require('lang/' . $language . '.php');
		}
		if(isset($lang[$string]) && $lang[$string]!='') {
			return $lang[$string];
		} else {
			return 'Language string error on ' . $string;
		}
	}
	
	public function getvar($var) {
		if(!isset($_GET[$var]) || $_GET[$var]=='') {
			$this->error(sprintf($this->lang('INVALID_VAR'),$var));
		} elseif($var === 'path' || $var === 'old'){
			// check path is under the right dir
			if($this->checkPath($this->uploadpath, $this->uploadpath . substr($_GET[$var],1))){
				$this->get[$var] = $_GET[$var];
				return true;
			} else {
				$this->error(sprintf($this->lang('INVALID_VAR'),$var));
			}
		} else {
			$this->get[$var] = $_GET[$var];
			return true;
		}
	}
	public function postvar($var) {
		if(!isset($_POST[$var]) || $_POST[$var]=='') {
			$this->error(sprintf($this->lang('INVALID_VAR'),$var));
		} elseif($var === 'currentpath'){
			// check path is under the right dir
			if($this->checkPath($this->uploadpath, $this->uploadpath . substr($_POST[$var],1))){
				$this->post[$var] = $_POST[$var];
				return true;
			} else 
				$this->error(sprintf($this->lang('INVALID_VAR'),$var));
		} else {
			$this->post[$var] = $_POST[$var];
			return true;
		}
	}
	
	public function getinfo() {
		$this->item = array();
		$this->item['properties'] = $this->properties;
		$this->get_file_info();
		$array = array(
			'URL'=> $this->uploadurl . $this->get['path'],
			'Path'=> $this->get['path'],
			'Filename'=>$this->item['filename'],
			'File Type'=>$this->item['filetype'],
			'Preview'=>$this->item['preview'],
			'Properties'=>$this->item['properties'],
			'Error'=>"",
			'Code'=>0
		);
		return $array;
	}
	
	public function getfolder() {
		$array = array();
		if(!is_dir($this->uploadpath . $this->get['path'])) {
			$this->error(sprintf($this->lang('DIRECTORY_NOT_EXIST'),$this->get['path']));
		}
		if(!$handle = opendir($this->uploadpath . $this->get['path'])) {
			$this->error(sprintf($this->lang('UNABLE_TO_OPEN_DIRECTORY'),$this->get['path']));
		} else {
			while (false !== ($file = readdir($handle))) {
				if($file != "." && $file != ".." && is_dir($file)) {
					$array[$this->get['path'] . $file] = array(
						'Path'=> $this->get['path'] . $file,
						'Filename'=>$file,
						'File Type'=>'dir',
						'Preview'=>$this->hostPrefixed($this->config['icons']['path'] . $this->config['icons']['directory']),
						'Properties'=>array(
							'Date Created'=>null, 
							'Date Modified'=>null, 
							'Height'=>null,
							'Width'=>null,
							'Size'=>null
						),
						'Error'=>"",
						'Code'=>0
					);
				} else if ($file != "." && $file != ".." && substr($file,0,1)!='.') {
					$this->item = array();
					$this->item['properties'] = $this->properties;
					$this->get_file_info($this->get['path'] . $file);
					
					if(!isset($this->params['type']) || (isset($this->params['type']) && $this->params['type']=='Images' && in_array($this->item['filetype'],$this->config['images']))) {
						$array[$this->uploadurl . $this->get['path'] . $file] = array(
							'Path'=> $this->get['path'] . $file,
							'Filename'=>$this->item['filename'],
							'File Type'=>$this->item['filetype'],
							'Preview'=>$this->item['preview'],
							'Properties'=>$this->item['properties'],
							'Error'=>"",
							'Code'=>0
						);
					}
				}
			}
			closedir($handle);
		}
		return $array;
	}
	
	public function rename() {
		if(substr($this->get['old'],-1,1)=='/') {
			$this->get['old'] = substr($this->get['old'],0,(strlen($this->get['old'])-1));
		}
		$tmp = explode('/',$this->get['old']);
		$filename = $tmp[(sizeof($tmp)-1)];
		$path = str_replace('/' . $filename,'',$this->uploadpath . $this->get['old']);
		
		if(!rename($this->uploadpath . $this->get['old'], $path . '/' . $this->get['new'])) {
			if(is_dir($this->get['old'])) {
				$this->error(sprintf($this->lang('ERROR_RENAMING_DIRECTORY'),$filename,$this->get['new']));
			} else {
				$this->error(sprintf($this->lang('ERROR_RENAMING_FILE'),$filename,$this->get['new']));
			}
		}
		$array = array(
			'Error'=>"",
			'Code'=>0,
			'Old Path'=>$this->get['old'],
			'Old Name'=>$filename,
			'New Path'=>$path . '/' . $this->get['new'],
			'New Name'=>$this->get['new']
		);
		return $array;
	}
	
	public function delete() {
		if(authDelete()){
			if(is_dir($this->uploadpath . $this->get['path'])) {
				$this->unlinkRecursive($this->uploadpath . $this->get['path']);
				$array = array(
					'Error'=>"",
					'Code'=>0,
					'Path'=>$this->get['path'].'/'
				);
				return $array;
			} else if(file_exists($this->uploadpath . $this->get['path'])) {
				unlink($this->uploadpath . $this->get['path']);
				$array = array(
					'Error'=>"",
					'Code'=>0,
					'Path'=>$this->get['path']
				);
				return $array;
			} else {
				$this->error(sprintf($this->lang('INVALID_DIRECTORY_OR_FILE')));
			}
		} else {
			$this->error(sprintf($this->lang('AUTHORIZATION_REQUIRED')));
		}
	}
	
	public function add() {
		$this->setParams();
		if(!isset($_FILES['newfile']) || !is_uploaded_file($_FILES['newfile']['tmp_name'])) {
			$this->error(sprintf($this->lang('INVALID_FILE_UPLOAD')),true);
		}
		if(($this->config['upload']['size']!=false && is_numeric($this->config['upload']['size'])) && ($_FILES['newfile']['size'] > ($this->config['upload']['size'] * 1024 * 1024))) {
			$this->error(sprintf($this->lang('UPLOAD_FILES_SMALLER_THAN'),$this->config['upload']['size'] . 'Mb'),true);
		}
		if($this->config['upload']['imagesonly'] || (isset($this->params['type']) && $this->params['type']=='Images')) {
			if(!($size = @getimagesize($_FILES['newfile']['tmp_name']))){
				$this->error(sprintf($this->lang('UPLOAD_IMAGES_ONLY')),true);
			}
			if(!in_array($size[2], array(1, 2, 3, 7, 8))) {
				$this->error(sprintf($this->lang('UPLOAD_IMAGES_TYPE_JPEG_GIF_PNG')),true);
			}
		}
		$_FILES['newfile']['name'] = $this->cleanString($_FILES['newfile']['name'],array('.','-'));
		if(!$this->config['upload']['overwrite']) {
			$_FILES['newfile']['name'] = $this->checkFilename($$this->uploadpath . $this->post['currentpath'], $_FILES['newfile']['name']);
		}
		if(move_uploaded_file($_FILES['newfile']['tmp_name'], $this->uploadpath . $this->post['currentpath'] . $_FILES['newfile']['name']) === FALSE){
			$this->error("Failed to move upload",true);
		}
		$response = array(
			'Path'=>$this->post['currentpath'],
			'Name'=>$_FILES['newfile']['name'],
			'Error'=>"",
			'Code'=>0
		);
		echo '<textarea>' . json_encode($response) . '</textarea>';
		die();
	}
	
	public function addfolder() {
		if(is_dir($this->uploadpath . $this->get['path'] . $this->get['name'])) {
			$this->error(sprintf($this->lang('DIRECTORY_ALREADY_EXISTS'),$this->get['name']));
			
		}
		if(!mkdir($this->uploadpath . $this->get['path'] . $this->get['name'],0755)) {
			$this->error(sprintf($this->lang('UNABLE_TO_CREATE_DIRECTORY'),$this->get['name']));
		}
		$array = array(
			'Parent'=> $this->get['path'],
			'Name'=>$this->get['name'],
			'Error'=>"",
			'Code'=>0
		);
		return $array;
	}
	
	public function download() {
		if(isset($this->get['path']) && $this->get['path']) {
			header("Content-type: application/force-download");
			header("Content-Transfer-Encoding: Binary");
			header("Content-length: ".filesize($this->uploadpath . $this->get['path']));
			header('Content-Type: application/octet-stream');
			$tmp = explode('/',$this->get['path']);
			$filename = $tmp[(sizeof($tmp)-1)];
			header('Content-Disposition: attachment; filename="' . $filename . '"');
			readfile($this->uploadpath . $this->get['path']);
		} else {
			$this->error(sprintf($this->lang('FILE_DOES_NOT_EXIST'),$this->get['path']));
		}
	}
	
	private function setParams() {
		$tmp = $_SERVER['HTTP_REFERER'];
		$tmp = explode('?',$tmp);
		$params = array();
		if(isset($tmp[1]) && $tmp[1]!='') {
			$params_tmp = explode('&',$tmp[1]);
			if(is_array($params_tmp)) {
				foreach($params_tmp as $value) {
					$tmp = explode('=',$value);
					if(isset($tmp[0]) && $tmp[0]!='' && isset($tmp[1]) && $tmp[1]!='') {
						$params[$tmp[0]] = $tmp[1];
					}
				}
			}
		}
		
		// check path is under the right dir
		if(isset($params['path']) && $this->checkPath($this->uploadpath, substr($params['path'],2))){
			$params['path'] = NULL;
		}
		
		$this->params = $params;
	}
	
	
	private function get_file_info($path='',$return=array()) {
		if($path=='') {
			$path = $this->get['path'];
		}
		$tmp = explode('/',$path);
		$this->item['filename'] = $tmp[(sizeof($tmp)-1)];
		
		$tmp = explode('.',$this->item['filename']);
		$this->item['filetype'] = $tmp[(sizeof($tmp)-1)];
		$this->item['filemtime'] = filemtime($this->uploadpath . $path);
		$this->item['filectime'] = filectime($this->uploadpath . $path);
		
		$this->item['preview'] = $this->hostPrefixed($this->config['icons']['path'] . $this->config['icons']['default']); // @simo
		
		if(is_dir($this->uploadpath . $path)) {
			
			$this->item['preview'] = $this->config['icons']['path'] . $this->config['icons']['directory'];
			
		} else if(in_array($this->item['filetype'],$this->config['images'])) {
			
			$this->item['preview'] = $this->hostPrefixed($this->uploadurl.$path); // @simo
			//if(isset($get['getsize']) && $get['getsize']=='true') {
				list($width, $height, $type, $attr) = getimagesize($this->uploadpath . $path);
				$this->item['properties']['Height'] = $height;
				$this->item['properties']['Width'] = $width;
				$this->item['properties']['Size'] = filesize($this->uploadpath . $path);
			//}
			
		} else if(file_exists($this->config['icons']['fspath'] . strtolower($this->item['filetype']) . '.png')) {
			
			$this->item['preview'] = $this->config['icons']['path'] . strtolower($this->item['filetype']) . '.png';
			$this->item['properties']['Size'] = filesize($this->uploadpath . $path);
			
		}
		
		$this->item['properties']['Date Modified'] = date($this->config['date'], $this->item['filemtime']);
		//$return['properties']['Date Created'] = date($config['date'], $return['filectime']); // PHP cannot get create timestamp
	}
	
	private function unlinkRecursive($dir,$deleteRootToo=true) {
		if(!$dh = @opendir($dir)) {
			return;
		}
		while (false !== ($obj = readdir($dh))) {
			if($obj == '.' || $obj == '..') {
				continue;
			}
			
			if (!@unlink($dir . '/' . $obj)) {
				$this->unlinkRecursive($dir.'/'.$obj, true);
			}
		}
		
		closedir($dh);
		
		if ($deleteRootToo) {
			@rmdir($dir);
		}
		return;
	}
	
	private function cleanString($string, $allowed = array()) {
        $allow = null;
        if (!empty($allowed)) {
            foreach ($allowed as $value) {
                $allow .= "\\$value";
            }
        }

        if (is_array($string)) {
            $cleaned = array();
            foreach ($string as $key => $clean) {
                $cleaned[$key] = preg_replace("/[^{$allow}a-zA-Z0-9]/", '', $clean);
            }
        } else {
            $cleaned = preg_replace("/[^{$allow}a-zA-Z0-9]/", '', $string);
        }
        return $cleaned;
    }
	
	private function checkFilename($path,$filename,$i='') {
		if(!file_exists($path . $filename)) {
			return $filename;
		} else {
			$_i = $i;
			$tmp = explode(/*$this->config['upload']['suffix'] . */$i . '.',$filename);
			if($i=='') {
				$i=1;
			} else {
				$i++;
			}
			$filename = str_replace($_i . '.' . $tmp[(sizeof($tmp)-1)],$i . '.' . $tmp[(sizeof($tmp)-1)],$filename);
			return $this->checkFilename($path,$filename,$i);
		}
	}
	
	// @simo
	private function hostPrefixed($filepath) {
	  if(isset($this->config['add_host']) && $this->config['add_host'] == true) {
	    return 'http://' . $_SERVER['HTTP_HOST'] . '/'. $filepath;
	  } else {
	    return $filepath; // @jason
	  }
	}
	
	private function checkPath($dir, $path){
		$canonicalDir = realpath($dir);
		$canonicalPath = realpath($path);
		return substr($canonicalPath, 0, strlen($canonicalDir)) == $canonicalDir;
	}
}

?>