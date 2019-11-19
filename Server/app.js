Ext.define('list_model', {
    extend: 'Ext.data.Model',
    fields: [
	{ name: 'NAME', type: 'string' }
]
});

Ext.define('stanok_model', {
    extend: 'Ext.data.Model',
    fields: [
	{ name: 'NAME', type: 'string' },
	{ name: 'VALUE', type: 'string' }
]
});


Ext.define('obor_model', {
	extend: 'Ext.data.Model',
	fields: [
	{ name: 'INVN'  , type: 'string' }
	,{ name: 'CEH'  , type: 'string' }
	,{ name: 'ZAVOD' , type: 'string' }
    ,{ name: 'MGROUP', type: 'string' }
	,{ name: 'NAIM' , type: 'string' }
	,{ name: 'SMEN' , type: 'string' }
	,{ name: 'KRIT' , type: 'bool' }
	,{ name: 'KZV'  , type:'string'}
	,{ name: 'KDV'  , type:'string'}
	,{ name: 'KPV'  , type:'string'}
	,{ name: 'KCHV' , type:'string'}
	,{ name: 'OEE'  , type:'string'}
	,{ name: 'TEEPO', type:'string'}
	,{ name: 'MVO'  , type:'string'}
	,{ name: 'TEEPA', type:'string'}
	,{ name: 'MVA'  , type:'string'}
	,{ name: 'NER_FULL'  , type:'string'}
	,{ name: 'NED_FULL'  , type:'string'}
    ,{ name: 'NEZ_FULL'  , type:'string'}
	,{ name: 'NETERR'   , type:'string'}
    ,{ name: 'SO0'   , type:'string'}
    ,{ name: 'SO2'   , type:'string'}
    ,{ name: 'SO3'   , type:'string'}
    ,{ name: 'SO4'   , type:'string'}
    ,{ name: 'SO5'   , type:'string'}
    ,{ name: 'SO6'   , type:'string'}
    ,{ name: 'SO7'   , type:'string'}
    ,{ name: 'SO8'   , type:'string'}
    ,{ name: 'SO9'   , type:'string'}
    ,{ name: 'SO10'   , type:'string'}
    ,{ name: 'SO11'   , type:'string'}
    ,{ name: 'SO12'   , type:'string'}
    ,{ name: 'SO13'   , type:'string'}
    ,{ name: 'SO14'   , type:'string'}
    ,{ name: 'SO15'   , type:'string'}
    ,{ name: 'SO16'   , type:'string'}
    ,{ name: 'SO17'   , type:'string'}
    ,{ name: 'SO18'   , type:'string'}
    ,{ name: 'SO19'   , type:'string'}
    ,{ name: 'SO20'   , type:'string'}
    ,{ name: 'SO21'   , type:'string'}
    ,{ name: 'SO22'   , type:'string'}
    ,{ name: 'SO23'   , type:'string'}
    ,{ name: 'SO24'   , type:'string'}

]
});

Ext.define('remont_model', {
    extend: 'Ext.data.Model',
    fields: [
	{ name: 'INVN', type: 'string' }
	, { name: 'CEH', type: 'string' }
	, { name: 'ZAVOD', type: 'string' }
	, { name: 'NAIM', type: 'string' }
    , { name: 'SO_DATE', type: 'string' }
    , { name: 'SO', type: 'string' }
    , { name: 'NAME', type: 'string' }
]
});

var intervalID=0;


var store_obor = Ext.create('Ext.data.Store', {
	model: 'obor_model',
	autoLoad: false,
	autoSync: false,
	//remoteSort: true,
	//remoteFilter: true,
	//pageSize: 30,
	proxy: {
		type: 'ajax',
		url: 'obor.aspx',
		reader: {
			type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
		},
		listeners: {
			exception: function (proxy, response, operation) {
			    /*
			    Ext.MessageBox.show({
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

var store_obor_map= Ext.create('Ext.data.Store', {
	model: 'obor_model',
	autoLoad: false,
	autoSync: false,
	//remoteSort: true,
	//remoteFilter: true,
	//pageSize: 30,
	proxy: {
		type: 'ajax',
		url: 'obor.aspx',
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

var store_remont = Ext.create('Ext.data.Store', {
    model: 'remont_model',
    autoLoad: false,
    autoSync: false,
    //remoteSort: true,
    //remoteFilter: true,
   // pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'remont.aspx',
        reader: {
          type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
        },
        listeners: {
            exception: function (proxy, response, operation) {
               /* Ext.MessageBox.show({
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

var store_now = Ext.create('Ext.data.Store', {
    model: 'remont_model',
    autoLoad: false,
    autoSync: false,
    //remoteSort: true,
    //remoteFilter: true,
   // pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'obnow.aspx',
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



var store_zavod = Ext.create('Ext.data.Store', {
    model: 'list_model',
    autoLoad: true,
    autoSync: false,
  //  remoteSort: true,
  //  remoteFilter: true,
  //  pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'zavod.aspx',
        reader: {
            type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
        },
        listeners: {
            exception: function (proxy, response, operation) {
               /* Ext.MessageBox.show({
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

var store_ceh = Ext.create('Ext.data.Store', {
    model: 'list_model',
    autoLoad: true,
    autoSync: false,
  //  remoteSort: true,
  //  remoteFilter: true,
  //  pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'ceh.aspx',
        reader: {
            type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
        },
        listeners: {
            exception: function (proxy, response, operation) {
               /* Ext.MessageBox.show({
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

var store_rceh = Ext.create('Ext.data.Store', {
    model: 'list_model',
    autoLoad: true,
    autoSync: false,
   // remoteSort: true,
   // remoteFilter: true,
  //  pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'ceh.aspx',
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

var store_nceh = Ext.create('Ext.data.Store', {
    model: 'list_model',
    autoLoad: true,
    autoSync: false,
   // remoteSort: true,
   // remoteFilter: true,
  //  pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'ceh.aspx',
        reader: {
            type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
        },
        listeners: {
            exception: function (proxy, response, operation) {
               /* Ext.MessageBox.show({
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


var store_stanok = Ext.create('Ext.data.Store', {
    model: 'stanok_model',
    autoLoad: true,
    autoSync: false,
    proxy: {
        type: 'ajax',
        url: 'stanok.aspx',
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




var store_mceh = Ext.create('Ext.data.Store', {
    model: 'list_model',
    autoLoad: true,
    autoSync: false,
   // remoteSort: true,
   // remoteFilter: true,
  //  pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'ceh.aspx',
        reader: {
            type: 'json'
		, root: 'data'
		, successProperty: 'success'
		, messageProperty: 'msg'
        },
        listeners: {
            exception: function (proxy, response, operation) {
               /* Ext.MessageBox.show({
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

var store_grp = Ext.create('Ext.data.Store', {
    model: 'list_model',
    autoLoad: true,
    autoSync: false,
  //  remoteSort: true,
  //  remoteFilter: true,
 //   pageSize: 30,
    proxy: {
        type: 'ajax',
        url: 'mgroup.aspx',
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

 var enumYNA = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['(Все)',-1]
,['Нет',0]
,['Да',1]
 ]});
 
 var enumS = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['(Все)',-1]
,['Периодический',0]
,['Односменный',1]
,['Двухсменный',2]
,['Трёхсменный',3]
,['Четырёхсменный',4]
 ]});
 
 
 var enumSO = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['(Все)',-1]
,['Ожидание ввода оператора, сбой',0]
,['Односменный',1]
,['Праздники и выходные',2]
,['Обед',3]
,['Плановый ремонт централизованными службами',4]
,['Аварийный ремонт централизованными службами',5]
,['Отсутствие задания',6]
,['Отсутствие материала, заготовок, деталей',7]
,['Отсутствие инструмента, оснастки, вспомогательного оборудования',8]
,['Отсутствие крана, транспорта',9]
,['Отсутствие оператора в связи с необеспеченностью',10]
,['Неявка оператора (б.лист, отпуск, командировка)',11]
,['Отсутствие энергоносителей',12]
,['Отсутствие сотрудника ОТК',13]
,['Отсутствие конструктора, технолога или ожидание его решения',14]
,['Естественные надобности',15]
,['Ознакомление с работой, документацией и инструктаж',16]
,['Переналадка оборудования, получение инструмента до начала работы, снятие-сдача по окончании работы',17]
,['Работа с управляющей программой',18]
,['Установка, выверка, снятие детали',19]
,['"Изменение режимов, смена инструмента, приспособления',20]
,['Контроль на рабочем месте',21]
,['Уборка, осмотр оборудования, чистка-смазка станка',22]
,['Сборочные операции',23]
,['Работа по карте несоответствий',24]
,['Нерабочее время по графику согласно сменности',25]

 ]});

var enumSR = Ext.create('Ext.data.ArrayStore', {
    fields: [{ name: 'name' }, { name: 'value', type: 'int'}], data: [
['(Все)', -1]
, ['Плановый', 4]
, ['Аварийный', 5]
 ]
});

 
if( typeof(Date.prototype.toLocaleFormat)=='undefined'){
    Date.prototype.toLocaleFormat = function(format) {
	    var f = {y : (this.getYear())%100 ,Y : this.getYear() + 1900,m : this.getMonth() + 1,d : this.getDate(),H : this.getHours(),M : this.getMinutes(),S : this.getSeconds()}
	    for(var k in f)
	        format = format.replace('%' + k, f[k] < 10 ? "0" + f[k] : f[k]);
	    return format;
	};
}

function myDateRenderer(value, metaData, record, row, col, store, gridView) 
{ 
    if(value==null) return '';
	var s='';
	if(Ext.isDate(value)){
		s = value.toLocaleFormat('%Y-%m-%d %H:%M:%S');
	}else{
		s = new String(value);
	}
	
	var svalue='';
	if (s !=''){
		  var parts2 = s.split(' ');
		  var dparts2  =parts2[0].split('-');
		  var hparts2 =parts2[1].split(':');
		  svalue=dparts2[0]+'-'+ dparts2[1] +'-'+ dparts2[2]+ ' '+hparts2[0] +':'+hparts2[1] +':'+ hparts2[2];
	}

    return svalue;
}


function myTSRenderer(value, metaData, record, row, col, store, gridView) 
{ 
    if(value==null) return '';
	var s='';
	s = new String(value);
	
	
	var svalue='';
	if (s !=''){
		  var parts = s.split('.');
		  if(parts.length==3){
		   svalue=parts[0] +'сут. ' + parts[1] ;
		  }
		  if(parts.length==2){
		   svalue=parts[0];
		  }
		  if(parts.length==1){
			svalue=s
		  }
		  
	}

    return svalue;
}

var last_s="";
var last_k =""; 
var last_z = "";
var last_c = "";
var last_g = "";
var last_rz = "";
var last_rc = "";
var last_rso = "";

var last_f = "";
var last_t = "";

var last_nz = "";
var last_nso="";
var last_nc = "";
var contentPanel;

function reloadObor(){
	store_obor.load({ params: { Z: last_z, G: last_g, C: last_c, F:last_f, T:last_t,K:last_k,S:last_s} });
}

function GetOborFilter(){
		var p1 = Ext.create('Ext.panel.Panel', {
		    layout: {
		        type: 'vbox',
		        pack: 'center',
		        align: 'stretch',
		        padding: 5
		    },
		    autoScroll: true,
			
			items: [
                    {
                        xtype: 'combobox',
                        store: store_zavod,
                        itemId: 'obor_zavod',
                        displayField: 'NAME',
                        valueField: 'NAME',
                        fieldLabel: 'Завод',
						emptyText:'Завод',
						labelAlign:'top',
                        editable: false,
                        queryMode: 'local',
	
                        listeners: {
                            select: function (combo, records, eOpts) {
                                p1.down('#obor_ceh').clearValue();
                                p1.down('#obor_grp').clearValue();
                                last_c = "";
                                last_g = "";
                                store_ceh.load({ params: { Z: records[0].get('NAME')} });
                                store_grp.load({ params: { Z: records[0].get('NAME')} });
                                last_z = records[0].get('NAME');
                            }
                        }
                    },
                        {
                            xtype: 'combobox',
                            store: store_grp,
                            itemId: 'obor_grp',
                            displayField: 'NAME',
                            valueField: 'NAME',
                            fieldLabel: 'Группа',
							emptyText:'Группа',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',

                            listeners: {
                                select: function (combo, records, eOpts) {
                                    last_g = records[0].get('NAME');
									reloadObor();
                                }
                            }
                        },

                        {
                            xtype: 'combobox',
                            store: store_ceh,
                            itemId: 'obor_ceh',
                            displayField: 'NAME',
                            valueField: 'NAME',
                            fieldLabel: 'Цех',
							emptyText:'Цех',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',
	
                            listeners: {
                                select: function (combo, records, eOpts) {
                                    last_c = records[0].get('NAME');
									reloadObor();
                                }
                            } 
                        },
						{

						xtype:  'datefield',
						labelAlign:'top',
						format:'d/m/Y',
						submitFormat:'Y-m-d H:i:s',
						value:  '',
						name:  'obor_dfrom',
						itemId: 'obor_dfrom',
						fieldLabel: 'C',
						emptyText: 'не задано',
						editable: false,

				 		submitEmptyText: false,
						listeners:{
							change: function( fld, newValue, oldValue, eOpts ){
										var dfrom = newValue;
										if(dfrom==null ){
											last_f="";
											graph_date=new Date();
										}
										if(dfrom!=null ){
											last_f=myDateRenderer(dfrom);
											if(Ext.isDate(dfrom))
												graph_date=dfrom;
											else{
												var dsstr = new String(dfrom);
												var rawDate = dsstr.split('-');
											    graph_date = new Date(rawDate[0], rawDate[1]-1, rawDate[2]);
											}
												
										}
										
										if(	last_c!="" || last_g!="" || last_k!="" || last_s!=""	)
											reloadObor();
										
							}
						}
						}
						
					
						,{

						xtype:  'datefield',
						labelAlign:'top',
						format:'d/m/Y',
						submitFormat:'Y-m-d H:i:s',
						value:  '',
						name:  'obor_dto',
						itemId: 'obor_dto',
						fieldLabel: 'По',
						emptyText: 'не задано',
						editable: false,
	
						submitEmptyText: false,
						listeners:{
							change: function( fld, newValue, oldValue, eOpts ){
										var dto = newValue;
									
										
										if(dto==null ){
											last_t="";
										}
										if( dto!=null ){
											last_t=myDateRenderer(dto);
										}
											if(	last_c!="" || last_g!="" || last_k!="" || last_s!=""	)
											reloadObor();
										
							}
						}
						}
						,
                        {
                            xtype: 'combobox',
                            store: enumS,
                            itemId: 'obor_sm',
                            displayField: 'name',
                            valueField: 'value',
                            fieldLabel: 'Сменность',
							emptyText:'Сменность',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',
				
                           
                            listeners: {
                                select: function (combo, records, eOpts) {
                                    last_s = records[0].get('value');
									if (last_c!="" || last_g !="" || last_k !="")
									reloadObor();
                                }
                            }
                        }
						
						,
                        {
                            xtype: 'combobox',
                            store: enumYNA,
                            itemId: 'obor_krit',
                            displayField: 'name',
                            valueField: 'value',
                            fieldLabel: 'Критичное',
							emptyText:'Критичное',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',
						
                           
                            listeners: {
                                select: function (combo, records, eOpts) {
                                    last_k = records[0].get('value');
									if (last_c!="" || last_g !="" || last_s !="")
									reloadObor();
                                }
                            }
                        }
                    ]
		}
	);
	return p1;
			
}

function OborRenderer(value, metaData, record, row, col, store, gridView) 
{
	/*if(record.get('SO5')!='00:00:00'){
		metaData.style='color:red;';
	}else{
		if(record.get('SO4')!='00:00:00'){
			metaData.style='color:#f000f0;';
		}
	}*/
	return value;
	 
}

function loadStanok(invn){
  
   contentPanel.removeAll();
   if(intervalID!=0){
		window.clearInterval(intervalID);
		intervalID=0;
    }
   contentPanel.add(GetStanok());
   Graph_invn=invn;
   store_stanok.load({ params: { SN: invn, F:last_f, T:last_t }});
   store_g_so.load({ params: { SN: invn, F:last_f }});
   store_g_sa.load({ params: { SN: invn, F: last_f} });
   filterPanel.removeAll();
   filterPanel.add(GetLegends());
   filterPanel.setTitle('Информация');
}

function unloadStanok(){
   contentPanel.removeAll();
   if(intervalID!=0){
		window.clearInterval(intervalID);
		intervalID=0;
    }
    contentPanel.add(GetOborPanel());
    filterPanel.removeAll();
    filterPanel.add(GetOborFilter());
    filterPanel.setTitle('Фильтр');

}


function GetOborPanel() {
    var p1 = Ext.create('Ext.panel.Panel', 
	{
            //title: 'Оборудование',
			layout: 'fit',
			autoScroll:true,
			items: [
				{ xtype: 'grid',
					itemId: 'obor_grid',
					autoScroll:true,
					store: store_obor,
					columns: [
							{ text: 'Номер', dataIndex: 'INVN', width: 80, minWidth: 70, sortable: true, locked: true, renderer: OborRenderer },
							{ text: 'Зав.', dataIndex: 'ZAVOD', width: 70, minWidth: 50, sortable: true, renderer: OborRenderer },
							{ text: 'Цех', dataIndex: 'CEH', width: 60, minWidth: 50, sortable: true, renderer: OborRenderer },
							
							{ text: ' ', width:24, minWidth:20,sortable: false, xtype:'actioncolumn', items:[ {icon:'./icons/zoom.png', 
								handler:  function(grid, rowIndex, colIndex) {
				                    		var rec = grid.getStore().getAt(rowIndex);
                    						loadStanok(rec.get('INVN'));
								}
							}
							] 
							},
							{ text: 'Название', dataIndex: 'NAIM', minWidth:150, flex: 1, sortable: true , renderer:OborRenderer},
							{ text: 'Сменность', dataIndex: 'SMEN', minWidth:90,  sortable: true , renderer:OborRenderer},
							{ text: 'Кр.', width: 55,  xtype: 'checkcolumn', dataIndex: 'KRIT', minWidth: 40, disabled: true, sortable: true },
				
							
							{   text:'Кдв',dataIndex:'KDV', inWidth:30,sortable:false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']} 
							},
							
							{ text: 'Кзв', dataIndex: 'KZV', minWidth: 30, sortable: false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']} },
                            { text: 'Кпв', dataIndex: 'KPV', minWidth: 30, sortable: false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']} },
                            { text: 'Кчв', dataIndex: 'KCHV', minWidth: 30, sortable: false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']} }, 
							{ text: 'ОЕЕ',   dataIndex: 'OEE'  , minWidth: 30, sortable: false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']}},
							{ text: 'ТЕЕРо', dataIndex: 'TEEPO', minWidth: 30, sortable: false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']} },
                            { text: 'ТЕЕРа', dataIndex: 'TEEPA', minWidth: 30, sortable: false ,xtype: 'widgetcolumn',  
								widget: {xtype: 'progressbarwidget',	textTpl: ['{value:number("0.000")}']} },
							{ text: 'МВа',   dataIndex: 'MVA'  , minWidth: 30, sortable: true , renderer:myTSRenderer},
                            { text: 'МВо', dataIndex: 'MVO', minWidth: 30, sortable: true , renderer:myTSRenderer},
							{ text: 'c.Ошибк', dataIndex: 'NETERR'  , minWidth: 60, sortable: true , renderer:myTSRenderer},
							{ text: 'Нераб', dataIndex: 'NER_FULL'   , minWidth: 60, sortable: true , renderer:myTSRenderer} 

						]
				}
			]
		}
      
    );

    return p1;
}

function GetRemontFilter(){
		var p1 = Ext.create('Ext.panel.Panel', {
			 layout: {
				type: 'vbox',
				pack: 'center',
				align: 'stretch',
				padding :5
			},
			autoScroll:true,
			items: [
					{
						xtype: 'combobox',
						store: store_zavod,
						itemId: 'rem_zavod',
						displayField: 'NAME',
						valueField: 'NAME',
					    fieldLabel: 'Завод',
						emptyText:'Завод',
						labelAlign:'top',
                        editable: false,
                        queryMode: 'local',
				
						listeners: {
							select: function (combo, records, eOpts) {
								p1.down('#rem_ceh').clearValue();
								store_rceh.load({ params: { Z: records[0].get('NAME')} });
								store_remont.load({ params: { C: last_rc, Z: last_rz, SO: last_rso} });
								last_rz = records[0].get('NAME');
								last_rc="";
							}
						}
					},
					  {
						  xtype: 'combobox',
						  store: store_rceh,
						  itemId: 'rem_ceh',
						  displayField: 'NAME',
						  valueField: 'NAME',
						    fieldLabel: 'Цех',
							emptyText:'Цех',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',
					
						  listeners: {
							  select: function (combo, records, eOpts) {
							      last_rc=records[0].get('NAME')
							      store_remont.load({ params: { C: last_rc, Z: last_rz, SO: last_rso} });
							  }
						  }
}
                      ,
					   {
					       xtype: 'combobox',
					       store: enumSR,
					       itemId: 'rem_so',
					       displayField: 'name',
					       valueField: 'value',
					       fieldLabel: 'Тип ремонта',
					       emptyText: 'Тип ремонта',
					       labelAlign: 'top',
					       editable: false,
					       queryMode: 'local',
					       listeners: {
					           select: function (combo, records, eOpts) {
					               last_rso = records[0].get('value');
					               store_remont.load({ params: { C: last_rc, Z: last_rz, SO: last_rso} });

					           }
					       }
					   }
					]
			}
			);
		return p1;
}

function remontRefresh(){
  if(last_rz!="" && last_rc!="")	
	store_remont.load({ params: { C:last_rc , Z: last_rz} });
}

function RemontRenderer(value, metaData, record, row, col, store, gridView) 
{
	if(record.get('SO')=='5'){
		metaData.style='color:#FF0000;';
	}else{
		metaData.style='color:#000000;';
	}
	return value;
	 
}


function GetRemontPanel() {
	var p1=Ext.create('Ext.panel.Panel', 
	{ 
			//title: 'Ремонты',
			layout: 'fit',
			autoScroll:true,
			items: [
				{ xtype: 'grid',
					itemId: 'remont_grid',
					store: store_remont,
					
					columns: [

						{ text: 'Номер', dataIndex: 'INVN', minWidth:50,sortable: true ,locked:true,renderer:RemontRenderer},
						{ text: 'Завод', dataIndex: 'ZAVOD',minWidth:50, sortable: true,renderer:RemontRenderer },
						{ text: 'Цех', dataIndex: 'CEH',minWidth:50, sortable: true ,renderer:RemontRenderer},
						{ text: 'Название', dataIndex: 'NAIM',minWidth:150, flex: 1,  sortable: true ,renderer:RemontRenderer},
						{ text: 'Дата', dataIndex: 'SO_DATE', minWidth:120,width: 160, sortable: true ,renderer:RemontRenderer},
						{ text: 'Состояние', dataIndex: 'NAME',minWidth:150, flex: 1,  sortable: true ,renderer:RemontRenderer}
						]
				}
			]
		}

	 );

		
	intervalID = window.setInterval(remontRefresh, 60000);
	
	return p1;
};



function nowRefresh(){
  if(last_nz!="" && last_nc!="")	
	store_now.load({ params: { C:last_nc , Z: last_nz ,SO: last_nso} });
}


function GetNowFilter(){
		var p1 = Ext.create('Ext.panel.Panel', {
			 layout: {
				type: 'vbox',
				pack: 'center',
				align: 'stretch',
				padding :5
			},
			autoScroll:true,
			items: [
					{
						xtype: 'combobox',
						store: store_zavod,
						itemId: 'now_zavod',
						displayField: 'NAME',
						valueField: 'NAME',
					    fieldLabel: 'Завод',
						emptyText:'Завод',
						labelAlign:'top',
                        editable: false,
                        queryMode: 'local',
				
						listeners: {
							select: function (combo, records, eOpts) {
								p1.down('#now_ceh').clearValue();
								last_nc='';
								store_nceh.load({ params: { Z: records[0].get('NAME')} });
								last_nz = records[0].get('NAME');
								store_now.load({ params: { C: last_nc, Z: last_nz,SO: last_nso} });
							}
						}
					},
					  {
						  xtype: 'combobox',
						  store: store_nceh,
						  itemId: 'now_ceh',
						  displayField: 'NAME',
						  valueField: 'NAME',
						    fieldLabel: 'Цех',
							emptyText:'Цех',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',
					
						  listeners: {
							  select: function (combo, records, eOpts) {
								  last_nc=records[0].get('NAME');
								  store_now.load({ params: { C: last_nc, Z: last_nz,SO: last_nso} });
								  
							  }
						  }
					  },
					   {
						  xtype: 'combobox',
						  store: enumSO,
						  itemId: 'now_so',
						  displayField: 'name',
						  valueField: 'value',
						  fieldLabel: 'Статус',
						  emptyText:'Статус',
						  labelAlign:'top',
						  editable: false,
						queryMode: 'local',
						  listeners: {
							  select: function (combo, records, eOpts) {
								  last_nso=records[0].get('value');
								  store_now.load({ params: { C: last_nc, Z: last_nz,SO: last_nso} });
								 
							  }
						  }
					  }
					]
			}
			);
		
		intervalID = window.setInterval(nowRefresh, 60000);
		return p1;
}

function NowRenderer(value, metaData, record, row, col, store, gridView) 
{
	if(record.get('SO')=='5'){
		metaData.style='color:red;';
	}
	if(record.get('SO')=='4'){
		metaData.style='color:#f000f0;';
	}
	
	return value;
	 
}


function GetNowPanel() {
	var p1=Ext.create('Ext.panel.Panel', 
	{ 
			//title: 'Рабочие места',
			layout: 'fit',
			autoScroll:true,
			items: [
				{ xtype: 'grid',
					itemId: 'now_grid',
					store: store_now,
					
					columns: [

						{ text: 'Номер', dataIndex: 'INVN', minWidth:50,sortable: true ,locked:true,renderer:NowRenderer},
						{ text: 'Завод', dataIndex: 'ZAVOD',minWidth:50, sortable: true,renderer:NowRenderer },
						{ text: 'Цех', dataIndex: 'CEH',minWidth:50, sortable: true ,renderer:NowRenderer},
						{ text: 'Название', dataIndex: 'NAIM',minWidth:150, flex: 1,  sortable: true ,renderer:NowRenderer},
						{ text: 'Дата', dataIndex: 'SO_DATE', minWidth:120,width: 160, sortable: true ,renderer:NowRenderer},
						{ text: 'Состояние', dataIndex: 'NAME',minWidth:150, flex: 1,  sortable: true ,renderer:NowRenderer}
						]
				}
			]
		}

	 );

	return p1;
};

var map_panel;

function GetMaptFilter(){
		var p1 = Ext.create('Ext.panel.Panel', {
        	 layout: {
				type: 'vbox',
				pack: 'center',
				align: 'stretch',
				padding :5
			},
			autoScroll:true,
			items: [
					{
						xtype: 'combobox',
						store: store_zavod,
						itemId: 'm_zavod',
						displayField: 'NAME',
						valueField: 'NAME',
					    fieldLabel: 'Завод',
						emptyText:'Завод',
						labelAlign:'top',
                        editable: false,
                        queryMode: 'local',
						//flex:1,
						//width:145,
						//minWidth:145,
						//maxWidth:145,
						listeners: {
							select: function (combo, records, eOpts) {
							
								p1.down('#m_ceh').clearValue();
								store_mceh.load({ params: { Z: records[0].get('NAME'),A:0} });
								
							}
						
						}
					},
					  { 
						  xtype: 'combobox',
						  store: store_mceh,
						  itemId: 'm_ceh',
						  displayField: 'NAME',
						  valueField: 'NAME',
						    fieldLabel: 'Цех',
							emptyText:'Цех',
							labelAlign:'top',
                            editable: false,
                            queryMode: 'local',
							//flex:1,
							//width:145,
							//minWidth:145,
							//maxWidth:145,
						  listeners: {
							  select: function (combo, records, eOpts) {
								 //combo.setWidth(145);
								  var src="map.html?C=" +records[0].get('NAME')+"&W="+(map_panel.getWidth()-34)+"&H="+(map_panel.getHeight()-34);
								  var frame=Ext.getElementById('map_frame_id')
								  frame.width=map_panel.getWidth()-30;
								  frame.height=map_panel.getHeight()-30;
								  frame.src =encodeURI(src);
								  //map_panel.setTitle('Цех:'+records[0].get('NAME'));
							  }
							 
							
						  }
					  }
					]
			}
			);
		return p1;
}

function GetMapPanel(){
	map_panel = Ext.create('Ext.panel.Panel', 
		{
			itemId:'map_panel',
			name:'map_panel',
			//title: 'Цех',
			layout: 'fit',
			autoScroll:true,
			padding:5,
			html: '<iframe id="map_frame_id" src="/rss/images/select.png" border="0" />',
			listeners:{
				resize: function(){
						  var frame=Ext.getElementById('map_frame_id')
						  frame.width=map_panel.getWidth()-30;
						  frame.height=map_panel.getHeight()-30;
						  }
			}
		}
	);
	return map_panel;

}





function GetStanok(){

		var p1=Ext.create('Ext.panel.Panel', 
	{ 
			//title: 'Данные по станку',
			layout: 'fit',
			autoScroll:true,
			items: [
				{
					xtype:'panel',
					layout:'accordion',
					items:[
						{   
							title:'Данные',
							xtype: 'grid',
							itemId: 'stanok_grid',
							store: store_stanok,
							tbar:[
								{
								  xtype:'button',
								  text: 'К списку оборудования',
								  itemId: 'cmd_back',
								  handler: 
								    function () {
										unloadStanok();
									}
							     }
						
							],
							columns: [

								{ text: 'Названия', dataIndex: 'NAME', minWidth:160,flex:1},
								{ text: 'Значение', dataIndex: 'VALUE',minWidth:80, flex:1 ,renderer:myTSRenderer}
								
								]
						}
						,GetGraph()
						
					]
				}
			]
		}

	 );

	return p1;

}




contentPanel=Ext.create('Ext.panel.Panel', { region:'center', layout:'fit' } );
filterPanel=Ext.create('Ext.panel.Panel', { hidden:true, title:'Фильтр',region:'west', layout:'fit' ,  width: 180,
	            collapsible: true,
                collapsed:false,
				autoScroll:true,
                //split: true ,
				titleCollapse :true,
				border:true} 
				);

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
	         xtype: 'panel',
	         //title: 'Меню',
	         layout: 'fit',
	         height: 35,
	         //collapsible: true,
	         //collapsed: false,
	         //autoScroll: true,
	         //titleCollapse: true,
	         //split: true,
	         items: [ {
					 xtype:'panel',
					 layout:'hbox',
					 items:[
			 
						{
							toggleGroup:'menu',
						    xtype: 'button',
						    scale: 'small',
						    text: 'Архив',
						    iconCls: 'icon-page_white_zip',
						    itemId: 'cmd_obor',
						    border: 1,
							flex:1,
						    style: {
						        borderColor: 'cyan',
						        borderStyle: 'solid'
						    },
						    handler: function () {
						        contentPanel.removeAll();
						        if(intervalID!=0){
									window.clearInterval(intervalID);
									intervalID=0;
						        }
						        contentPanel.add(GetOborPanel());
						        filterPanel.removeAll();
						        filterPanel.add(GetOborFilter());
						        filterPanel.setVisible(true);
						    }


						},
                        {
							toggleGroup:'menu',
                            xtype: 'button',
                            scale: 'small',
                            text: 'Цех',
                            iconCls: 'icon-house',
                            itemId: 'cmd_wrk',
						    border: 1,
							flex:1,
                            style: {
                                borderColor: 'cyan',
                                borderStyle: 'solid'
                            },
                            handler: function () {
                                contentPanel.removeAll();
                                if(intervalID!=0){
									window.clearInterval(intervalID);
									intervalID=0;
						        }
                                contentPanel.add(GetMapPanel());
                                filterPanel.removeAll();
                                filterPanel.add(GetMaptFilter());
                                filterPanel.setVisible(true);


                            }


                        },
                        {
							toggleGroup:'menu',
                            xtype: 'button',
                            scale: 'small',
                            text: 'Ремонты',
                            iconCls: 'icon-wrench',
                            itemId: 'cmd_rem',
	                        border: 1,
							flex:1,
                            style: {
                                borderColor: 'cyan',
                                borderStyle: 'solid'
                            },
                            handler: function () {
                                contentPanel.removeAll();
                                if(intervalID!=0){
									window.clearInterval(intervalID);
									intervalID=0;
						        }
                                contentPanel.add(GetRemontPanel());
                                filterPanel.removeAll();
                                filterPanel.add(GetRemontFilter());
                                filterPanel.setVisible(true);
                            }


                        }
						,
                        {
							toggleGroup:'menu',
                            xtype: 'button',
                            scale: 'small',
                            text: 'Станки',
                            iconCls: 'icon-page_white_gear',
                            itemId: 'cmd_now',
	                        border: 1,
							flex:1,
                            style: {
                                borderColor: 'cyan',
                                borderStyle: 'solid'
                            },
                            handler: function () {
                                contentPanel.removeAll();
                                if(intervalID!=0){
									window.clearInterval(intervalID);
									intervalID=0;
						        }
                                contentPanel.add(GetNowPanel());
                                filterPanel.removeAll();
                                filterPanel.add(GetNowFilter());
                                filterPanel.setVisible(true);
                            }


                        }
					
						
						
					]
					}

				]
	     },
            filterPanel,
			contentPanel


		]

	    }
		);
        AfterLoad();

     }
 }
);