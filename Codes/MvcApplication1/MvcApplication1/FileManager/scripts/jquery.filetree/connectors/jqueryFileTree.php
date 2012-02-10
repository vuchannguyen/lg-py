<?php
//
// jQuery File Tree PHP Connector
//
// Version 1.01
//
// Cory S.N. LaViska
// A Beautiful Site (http://abeautifulsite.net/)
// 24 March 2008
//
// History:
//
// 1.01 - updated to work with foreign characters in directory/file names (12 April 2008)
// 1.00 - released (24 March 2008)
//
// Output a list of files for jQuery File Tree
//

require('../../../connectors/php/filemanager.config.php');

if(auth()) {

	$root = $config['root'];
	
	$dir = urldecode($_POST['dir']);
	
	$path = NULL;
	$canonicalDir = realpath($root);
	$canonicalPath = realpath($root.$dir);
	if(substr($canonicalPath, 0, strlen($canonicalDir)) == $canonicalDir){
		$path = $canonicalPath;
	}
	
	if($path !== NULL && file_exists($path) ) {
		$files = scandir($path);
		natcasesort($files);
		if( count($files) > 2 ) { /* The 2 accounts for . and .. */
			echo "<ul class=\"jqueryFileTree\" style=\"display: none;\">";
			// All dirs
			foreach( $files as $file ) {
				if( file_exists($path .'/'. $file) && $file != '.' && $file != '..' && is_dir($path .'/'. $file) ) {
					echo "<li class=\"directory collapsed\"><a href=\"#\" rel=\"" . htmlentities($dir.$file) . "/\">" . htmlentities($file) . "</a></li>";
				}
			}
			// All files
			foreach( $files as $file ) {
				if( file_exists($path .'/'. $file) && $file != '.' && $file != '..' && !is_dir($path .'/'. $file) ) {
					$ext = preg_replace('/^.*\./', '', $file);
					echo "<li class=\"file ext_$ext\"><a href=\"#\" rel=\"" . htmlentities($dir.$file) . "\">" . htmlentities($file) . "</a></li>";
				}
			}
			echo "</ul>";	
		}
	}
}
?>
