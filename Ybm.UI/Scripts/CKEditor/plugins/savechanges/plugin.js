CKEDITOR.plugins.add('savechanges',
       {
           init: function (editor) {
               editor.on('contentDom', function () {
                   editor.document.on('keyup', function (event) {
                       $('#TemplateContent').val(editor.getData());
                       //$('#TemplateContent').click();
                       //$('#TemplateContent').blur();
                       //$('#TemplateContent').focus();
                       $('#TemplateContent').change();
                      
                   })
               })

           }
       });