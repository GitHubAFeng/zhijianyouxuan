/*
 * FCKeditor - The text editor for Internet - http://www.fckeditor.net
 * Copyright (C) 2003-2009 Frederico Caldeira Knabben
 *
 * == BEGIN LICENSE ==
 *
 * Licensed under the terms of any of the following licenses at your
 * choice:
 *
 *  - GNU General Public License Version 2 or later (the "GPL")
 *    http://www.gnu.org/licenses/gpl.html
 *
 *  - GNU Lesser General Public License Version 2.1 or later (the "LGPL")
 *    http://www.gnu.org/licenses/lgpl.html
 *
 *  - Mozilla Public License Version 1.1 or later (the "MPL")
 *    http://www.mozilla.org/MPL/MPL-1.1.html
 *
 * == END LICENSE ==
 *
 * Scripts related to the Link dialog window (see fck_link.html).
 */
 
 /***********************************************************
 * Edited by WuJian in 2009-11-26
 * http://luck0235.cnblogs.com
 ***********************************************************/

var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;

var FCK			= oEditor.FCK ;
var FCKLang		= oEditor.FCKLang ;
var FCKConfig	= oEditor.FCKConfig ;
var FCKRegexLib	= oEditor.FCKRegexLib ;
var FCKTools	= oEditor.FCKTools ;

//#### Dialog Tabs

// Set the dialog tabs.
dialog.AddTab( 'Info', FCKLang.DlgLnkInfoTab ) ;

// Function called when a dialog tag is selected.
function OnDialogTabChange( tabCode ){
	ShowE('divInfo'		, ( tabCode == 'Info' ) ) ;	
	dialog.SetAutoSize( true ) ;
}

var oParser = new Object() ;

// oLink: The actual selected link in the editor.
// 获取当前选中的链接，用于属性编辑
var oLink = dialog.Selection.GetSelection().MoveToAncestorNode( 'A' ) ;
if ( oLink )
	FCK.Selection.SelectNode( oLink ) ;

window.onload = function()
{
	/*
	// Translate the dialog box texts.
	oEditor.FCKLanguageManager.TranslatePage(document) ;
	*/
	
	// Load the selected link information (if any).
	LoadSelection() ;
	
	// Activate the "OK" button.
	dialog.SetOkButton( true ) ;
	
}

function LoadSelection(){
	if ( !oLink ) return ;
	
	// Get the actual Link href.
	var sHRef = oLink.getAttribute( '_fcksavedurl' ) ;
	if ( sHRef == null )
		sHRef = oLink.getAttribute( 'href' , 2 ) || '' ;
		
	GetE('linkUrl').value = sHRef ;
	GetE('linkText').value = oLink.innerHTML;
}

//提交验证
function validOk(){
	if(GetE("linkText").value.length > 0){
		if(GetE("linkUrl").value.length > 0){			
			return true;
		}
		else{
			alert("请输入链接地址！");
			GetE("linkUrl").focus();
			return false;
		}
	}
	else{
		alert("请输入文本内容！");
		GetE("linkText").focus();
		return false;
	}
}

//#### The OK button was hit.
function Ok(){
	
	if(validOk()){
		//sUri:链接地址
		//sInnerHtml:文本内容
		var sUri, sInnerHtml ;
	
		//?
		oEditor.FCKUndo.SaveUndoStep();
	
		//取值
		sUri = GetE("linkUrl").value;
		sInnerHtml = GetE("linkText").value;
	
		// If no link is selected, create a new one (it may result in more than one link creation - #220).
		// Create aLinks object, if selected, use [oLink] value; otherwise create a new
		var aLinks = oLink ? [ oLink ] : oEditor.FCK.CreateLink( sUri, true ) ;

		// If no selection, no links are created, so use the uri as the link text (by dom, 2006-05-26)
		// if selected, value was true ; created, value was false
		var aHasSelection = ( aLinks.length > 0 ) ;
		if ( !aHasSelection ){
			// Create a new (empty) anchor.
			aLinks = [ oEditor.FCK.InsertElement( 'a' ) ] ;
		}

		for ( var i = 0 ; i < aLinks.length ; i++ ){
			oLink = aLinks[i] ;
			
			// url
			oLink.href = sUri ;
			SetAttribute( oLink, '_fcksavedurl', sUri ) ;
		
			// text
			oLink.innerHTML = sInnerHtml ;		// Set (or restore) the innerHTML
			
			// target
			SetAttribute( oLink, 'target', '_blank' ) ;
		}

		// Select the (first) link.
		oEditor.FCKSelection.SelectNode( aLinks[0] );

		return true ;
	}
}