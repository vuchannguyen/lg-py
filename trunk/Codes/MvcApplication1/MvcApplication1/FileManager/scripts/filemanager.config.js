/*---------------------------------------------------------
  Configuration
---------------------------------------------------------*/

// Set this to the server side language you wish to use.
var lang = 'aspx'; // options: lasso, php, py, aspx

//// Set this to the directory you wish to manage.
//var fileRoot = '/uploads/';

var am = document.location.pathname.substring(1, document.location.pathname
  .lastIndexOf('/') + 1);

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

// Set this to the directory you wish to manage.
var fileRoot = '/uploads/';

var value = getQuerystring('location');
if (value == 'user1') {
    var fileRoot = '/uploads/Image/';
}

if (value == 'user2') {
    var fileRoot = '/uploads/My Folder/';
}

// Show image previews in grid views?
var showThumbs = true;
