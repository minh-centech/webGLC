/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Areas/assets/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Areas/assets/js/plugins/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Areas/assets/js/plugins/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Areas/assets/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Areas/assets/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Areas/assets/js/plugins/ckfinder/');
};
