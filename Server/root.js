Ext.application(
 {
     name: 'MyApp',

     launch: function () {
         var vPort = new Ext.container.Viewport(

	        {
	            renderTo: Ext.getBody(),
	            layout: 'border',
	            items: [
			    {
				    region: 'north',
				    xtype: 'component',
				    padding: 10,
				    height: 40,
				    html: '<b>Мониторинг</b> web клиент.'
			    },
                {
	                region: 'west',
		            title: 'Меню',
		            layout: {
		                type: 'table',
		                columns: 1,
		                tdAttrs: { style: 'padding: 5px 10px;' }
		            },
			        items: [
						{   
                            xtype: 'button',
                            scale:'large',
							text: 'Станочный парк',
							itemId: 'cmd_obor',
                            border: 5,
                            style: {
                                borderColor: 'cyan',
                                borderStyle: 'solid'
                            },
							handler: function(){
                            }

						
						},
                        {   
                            xtype: 'button',
                            scale: 'large',
							text: 'Рабочие места',
							itemId: 'cmd_wrk',
							border: 5,
							style: {
							    borderColor: 'cyan',
							    borderStyle: 'solid'
							},
							handler: function(){
                            }

						
						},
                        {   
                            xtype: 'button',
                            scale: 'large',
							text: 'Ремонты',
							itemId: 'cmd_rem',
							border: 5,
							style: {
							    borderColor: 'cyan',
							    borderStyle: 'solid'
							},
							handler: function(){
                            }

						
						}
					
					]
				}
				]
	        }
		);


     }
 }
);