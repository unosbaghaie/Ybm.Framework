$(function(){

    //modify buttons style
    $.fn.editableform.buttons = 
    '<button type="submit" class="btn btn-primary editable-submit btn-sm"><i class="fa fa-check"></i></button>' +
    '<button type="button" class="btn editable-cancel btn-sm"><i class="fa fa-times"></i></button>';         
    
    //editables 
    $('#Website').editable({
        type: 'text',
        url: '/UserProfile/UpdateUserProfileField',
        pk: 1,
        name: 'Website',
        title: 'Enter Website',
        ajaxOptions: {
            type: 'post'
        }
    });
      $('#Email').editable({
        type: 'text',
        pk: 1,
        name: 'username',
        title: 'Enter username'
    });



    
    $('#firstname').editable({
      validate: function(value) {
       if($.trim(value) == '') return 'This field is required';
     }
   });
    
    $('#sex').editable({
      prepend: "not selected",
      source: [
      {value: 1, text: 'Male'},
      {value: 2, text: 'Female'}
      ],
      display: function(value, sourceData) {
       var colors = {"": "gray", 1: "green", 2: "blue"},
       elem = $.grep(sourceData, function(o){return o.value == value;});

       if(elem.length) {
         $(this).text(elem[0].text).css("color", colors[value]);
       } else {
         $(this).empty();
       }
     }
   });
    
    $('#status').editable();
    
    $('#group').editable({
      showbuttons: false
    });

    $('#dob').editable();

    $('#comments').editable({
      showbuttons: 'bottom'
    });



    //inline


  $('#inline-username').editable({
     type: 'text',
     pk: 1,
     name: 'username',
     title: 'Enter username',
      //mode: 'inline'
     mode: 'popover'
   });
    
    $('#inline-firstname').editable({
      validate: function(value) {
       if($.trim(value) == '') return 'This field is required';
     },
        //mode: 'inline'
      mode: 'popover'
   });
    
    $('#inline-sex').editable({
      prepend: "not selected",
        //mode: 'inline'
      mode: 'popover',
      source: [
      {value: 1, text: 'Male'},
      {value: 2, text: 'Female'}
      ],
      display: function(value, sourceData) {
       var colors = {"": "gray", 1: "green", 2: "blue"},
       elem = $.grep(sourceData, function(o){return o.value == value;});

       if(elem.length) {
         $(this).text(elem[0].text).css("color", colors[value]);
       } else {
         $(this).empty();
       }
     }
   });
    
    //$('#inline-status').editable({mode: 'inline'});
    $('#inline-status').editable({ mode: 'popover' });
    
    $('#inline-group').editable({
      showbuttons: false,
        //mode: 'inline'
      mode: 'popover'
    });

    //$('#inline-dob').editable({mode: 'inline'});
    $('#inline-dob').editable({ mode: 'popover' });

    $('#inline-comments').editable({
      showbuttons: 'bottom',
        //mode: 'inline'
      mode: 'popover'
    });



  });