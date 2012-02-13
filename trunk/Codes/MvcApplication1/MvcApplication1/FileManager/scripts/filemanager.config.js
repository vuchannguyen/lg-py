/*---------------------------------------------------------
  Configuration
---------------------------------------------------------*/

// Set this to the server side language you wish to use.
var lang = 'aspx'; // options: lasso, php, py, aspx

// Set this to the directory you wish to manage.
var fileRoot = 'Empty';

function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}

var value = getQuerystring('location');
if (value == 'LOT') {
    var fileRoot = '/FileUpload/LOT/';
}

// Show image previews in grid views?
var showThumbs = true;
