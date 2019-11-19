Ext.define('graph_model', {
	extend: 'Ext.data.Model',
	fields: [
	{ name: 'ETIME'  , type: 'string' }
	,{ name: 'V'   , type:'number'}
    ,{ name: 'COLOR'   , type:'string'}
]
});

Ext.define('legend_model', {
	extend: 'Ext.data.Model',
	fields: [
	{ name: 'OPTYPE'  , type: 'string' }
	,{ name: 'NAME'   , type:'string'}
    ,{ name: 'COLOR'   , type:'string'}
]
});

var store_g_so = Ext.create('Ext.data.Store', {
	model: 'graph_model',
	autoLoad: false,
	autoSync: false,

	proxy: {
		type: 'ajax',
		url: 'g_so.aspx',
		reader: {
			type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
		},
		listeners: {
			exception: function (proxy, response, operation) {
				/*Ext.MessageBox.show({
					title: 'REMOTE EXCEPTION',
					msg: operation.getError(),
					icon: Ext.MessageBox.ERROR,
					buttons: Ext.Msg.OK
				});*/
			}
		}
	}
});


var store_legend = Ext.create('Ext.data.Store', {
	model: 'legend_model',
	autoLoad: false,
	autoSync: false,

	proxy: {
		type: 'ajax',
		url: 'legend.aspx',
		reader: {
			type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
		},
		listeners: {
			exception: function (proxy, response, operation) {
				/*Ext.MessageBox.show({
					title: 'REMOTE EXCEPTION',
					msg: operation.getError(),
					icon: Ext.MessageBox.ERROR,
					buttons: Ext.Msg.OK
				});*/
			}
		}
	}
});

var store_g_sa = Ext.create('Ext.data.Store', {
	model: 'graph_model',
	autoLoad: false,
	autoSync: false,

	proxy: {
		type: 'ajax',
		url: 'g_sa.aspx',
		reader: {
			type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
		},
		listeners: {
			exception: function (proxy, response, operation) {
				/*Ext.MessageBox.show({
					title: 'REMOTE EXCEPTION',
					msg: operation.getError(),
					icon: Ext.MessageBox.ERROR,
					buttons: Ext.Msg.OK
				});
                */
			}
		}
	}
});


function LegendRenderer(value, metaData, record, row, col, store, gridView) 
{
	metaData.style='color:white;'+'background-color:'+record.get('COLOR')+';';
	return value;
	 
}

var GraphShift=0;
var graph_date=new Date();
var Graph_invn="";
 function GetGraph() {
		GraphShift=0;

        
		var p1 = Ext.create('Ext.panel.Panel', {
		    layout: 'absolute',
		    autoScroll: true,
			marging:5,
			title:'Графики',
			tbar: 
			[	/*{
				  xtype:'button',
				  text: 'К списку оборудования',
				  itemId: 'cmd_back2',
				  handler: 
					function () {
						unloadStanok();
					}
				 },*/
				{
					//text: '<<',nCls
                    iconCls:'icon-rewind_blue',
					handler: function () {
						GraphShift=GraphShift-1;
							store_g_so.load({ params: { SN: Graph_invn, F:myDateRenderer(graph_date),A:GraphShift },  scope: this,
						callback: function(records, operation, success) {
							var chart = this.up('panel').down('#so_chart');
							chart.redraw();
						} });
						store_g_sa.load({ params: { SN: Graph_invn, F:myDateRenderer(graph_date),A:GraphShift },  scope: this,
						callback: function(records, operation, success) {
							 var chart = this.up('panel').down('#sa_chart');
						
							chart.redraw();
						}});
						var gd= this.up('panel').down('#graph_date_f');
						var to = new Date(graph_date.getFullYear(),graph_date.getMonth(),graph_date.getDate());
						to.setDate(to.getDate() + GraphShift); 
						gd.setValue(to.toLocaleFormat('%Y-%m-%d'));
					}
				},
				{
					xtype:'displayfield',
					itemId: 'graph_date_f',
					width:100,
					value: graph_date.toLocaleFormat('%Y-%m-%d'),
					
				},
				{
					//text: '>>',
                    iconCls:'icon-forward_blue',
					handler: function () {
						GraphShift=GraphShift+1;
						store_g_so.load({ params: { SN: Graph_invn, F:myDateRenderer(graph_date),A:GraphShift },  scope: this,
						callback: function(records, operation, success) {
							var chart = this.up('panel').down('#so_chart');
							chart.redraw();
						} });
						store_g_sa.load({ params: { SN: Graph_invn, F:myDateRenderer(graph_date),A:GraphShift },  scope: this,
						callback: function(records, operation, success) {
							 var chart = this.up('panel').down('#sa_chart');
							chart.redraw();
						}});
						var gd= this.up('panel').down('#graph_date_f');
						var to = new Date(graph_date.getFullYear(),graph_date.getMonth(),graph_date.getDate());
						to.setDate(to.getDate() + GraphShift); 
						gd.setValue(to.toLocaleFormat('%Y-%m-%d'));
					}
				}
	 
			],

    items: [ 
	
	{
        xtype: 'cartesian',
        width: '100%',
        height: 120,
		x:5,
		y:5,
		showMarkers :false,
		colors:['#333333'],
		itemId:'so_chart',
		id:'so_chart',
        store: store_g_so,
        background: 'white',
        interactions: {
            type: 'panzoom',
            zoomOnPanGesture: true,
			showOverflowArrows:true
        },
		 sprites: [{
                type: 'text',
                text: 'Данные оператора',
                fontSize: 22,
                width: 100,
                height: 30,
                x: 50, // the sprite x position
                y: 90  // the sprite y position
            }],
        series: [
            {
                type: 'line',
                xField: 'ETIME',
                yField: 'V',
                fill: true,
                smooth: true,
                style: {
                    lineWidth: 1
                },
                
                renderer: function (sprite, config, rendererData, index) {
				
					
                        var store = store_g_so,
                        storeItems = store.getData().items,
                        currentRecord = storeItems[index],
                     
                        changes = {};
						if(currentRecord){
							switch (config.type) {
							  
								case 'line':
									changes.strokeStyle = currentRecord.data['COLOR'];
									changes.fillStyle = currentRecord.data['COLOR'];
									break;
							}
						}
                    return changes;
                }
            }
        ],
        axes: [
            {
                type: 'numeric',
                position: 'left',
				majorTickSteps:1,
                fields: ['V'],
                minimum: 0,
				maximum:1,
				hidden:true
                
            },
            {
                type: 'category',
                position: 'top',
                fields: 'ETIME',
				grid:false,
				maxZoom:20,
				label: {
                    rotate: {
                        degrees: -45
                    }
                }
            }
        ]
    }
		,
	{
        xtype: 'cartesian',
        width: '100%',
        height: 120,
		x:5,
		y:115,
		showMarkers :false,
		colors:['#333333'],
		itemId:'sa_chart',
		id:'sa_chart',
        store: store_g_sa,
        background: 'white',
        interactions: {
            type: 'panzoom',
            zoomOnPanGesture: true,
			showOverflowArrows:true
        },
		 sprites: [{
                type: 'text',
                text: 'Данные автоматики',
                fontSize: 22,
                width: 100,
                height: 30,
                x: 50, // the sprite x position
                y: 40  // the sprite y position
            }],
        series: [
            {
                type: 'line',
                xField: 'ETIME',
                yField: 'V',
                fill: true,
                smooth: true,
                style: {
                    lineWidth: 1
                },
           
                renderer: function (sprite, config, rendererData, index) {
					
					
						var store = store_g_sa,
                        storeItems = store.getData().items,
                        currentRecord = storeItems[index],
                        changes = {};
					  	if(currentRecord){
							switch (config.type) {
							  
								case 'line':
									changes.strokeStyle = currentRecord.data['COLOR'];
									changes.fillStyle = currentRecord.data['COLOR'];
									break;
							}
						}
						return changes;
                }
            }
        ],
        axes: [
            {
                type: 'numeric',
                position: 'left',
				majorTickSteps:1,
                fields: ['V'],
                minimum: 0,
				maximum:1,
				hidden:true
                
            },
            {
                type: 'category',
                position: 'bottom',
                fields: 'ETIME',
				grid:false,
				maxZoom:20,
				label: {
                    rotate: {
                        degrees: 45
                    }
                }
            }
        ]
    }
	
	
	
	
	]
		});
		
        return p1;
    }


    function GetLegends(){
        var p1 = Ext.create('Ext.panel.Panel', {
		    layout: 'fit',
		    autoScroll: true,
			marging:0,
	
            items:[
            {
		       // title:'Значения цветов',
		        border:true,
		        xtype:'grid',
		
		        itemId:'legends',
		        store:store_legend,
		        columns:[
		
			       // { text: 'Источник', dataIndex: 'OPTYPE', minWidth:90, width:90,sortable: true},
			        { text: 'Легенда', dataIndex: 'NAME',  flex:1, sortable: true , renderer:LegendRenderer  },
			       // { text: 'Цвет', dataIndex: 'COLOR',minWidth:80, sortable: true , renderer:LegendRenderer }
							
		        ]
	        }
            ]
        }
    );
    store_legend.load();
    return p1;
    }
