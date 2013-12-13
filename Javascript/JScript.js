function GetRadWindow()
{
  var oWindow = null;
  if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
  else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)
  return oWindow;
}
function ShowWindow(windowName)
{
   if (window.radopen != null)
       window.radopen(null,windowName);
   else
       window.frameElement.radopen(null,windowName);
}
function getFullPath(node)
{
   var fullpath = '';
   if ( node.Parent == null)
       return node.get_text();
   else
       return getFullPath(node.Parent) + "/ " + node.get_text()
}