CKEDITOR.plugins.add( 'timestamp',
{
	init: function( editor )
	{
		editor.addCommand( 'insertTimestamp',
			{
				exec : function( editor )
				{    
				    var timestamp = new Date().toDateString();
					editor.insertHtml(  timestamp.toString() );
				}
			});

		editor.ui.addButton( 'Timestamp',
		{
			label: 'Insert Timestamp',
			command: 'insertTimestamp',
			icon: this.path + 'images/timestamp.png'
		} );
	}
} );