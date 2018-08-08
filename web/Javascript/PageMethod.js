// JScript File

var screenWidth, screenHeight;
screenWidth = screen.width;
screenHeight = screen.height;


function openWindow(url, name)
{
    features = 'maximized=yes,resizable=yes,directories=0,location=0,menubar=0,toolbar=0,status=0,dependent=0,screenX=0,screenY';

    var win = null;
    win = window.open(url, name, features);
    if (window.focus) { win.focus(); }
}
function openPopUp(url, name, w, h)
{
    popWidth = w;
    popHeight = h;
    popLeft = (screenWidth - popWidth)/2;
    popTop = (screenHeight - popHeight)/2;

    features = 'directories=0,location=0,menubar=0,toolbar=0,status=0,dependent=0,width=' + popWidth + 
               ',height=' + popHeight + 
               ',innerWidth=' + popWidth + 
               ',innerHeight=' + popHeight + 
               ',left=' + popLeft + 
               ',top=' + popTop;
    var win = null;
    win = window.open(url, name, features);
    if (window.focus) { win.focus(); }
}
function openPop(url, name, w, h) {
    popWidth = w;
    popHeight = h;
    popLeft = (screenWidth - popWidth) / 2;
    popTop = (screenHeight - popHeight) / 2;

    features = 'resizable=no, directories=0,location=0,menubar=0,toolbar=0,status=0,dependent=0, scrollbars=1, width=' + popWidth +
               ',height=' + popHeight +
               ',left=' + popLeft +
               ',top=' + popTop;
    var win = null;
    window.open(url, name, features);
   // if (window.focus) { win.focus(); }
}
function showMessage(msg)
{   
    document.getElementById('divMessenger').innerHTML=msg;
    if (msg != " ") 
     { 
         setTimeout("showMessage(' ')",15000);
     }
}