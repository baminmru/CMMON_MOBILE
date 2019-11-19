<%@ Page Language="VB" AutoEventWireup="false" CodeFile="map.aspx.vb" Inherits="map" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="leaflet.css" />
    <script src="leaflet.js" type="text/jscript"></script>
    <script src="jquery-2.1.1.min.js" type="text/jscript"></script>
    <script src="lll/Marker.Text.js"></script>
<%--	<link rel="stylesheet" href="lll/Control.FullScreen.css" />
	<script src="lll/Control.FullScreen.js"></script>--%>
    
</head>
<body>
    <form id="form1" runat="server">
      <div id="map" style='height:<% =GetH()%>px;'></div>
      <script type="text/javascript">
        var imageUrl = '/Server/images/<%=GetC()%>.png', imageBounds = [[-4.95, -6.4], [4.95, 6.4]];
        var map = L.map('map', { minZoom: 5, maxZoom: 10,attributionControl:false});  //.setView([27, 44], 3);
        map.attributionControl = false;
        L.imageOverlay(imageUrl, imageBounds, { opacity: 1 }).addTo(map); // , attribution: 'Цех:<%=GetC()%>'
        var stanki =[];
        map.setView([0, 0], 7);

//        // create fullscreen control
//        var fsControl = new L.Control.FullScreen();
//        // add fullscreen control to the map
//        map.addControl(fsControl);

//        // detect fullscreen toggling
//        map.on('enterFullscreen', function () {
//            if (window.console) window.console.log('enterFullscreen');
//        });
//        map.on('exitFullscreen', function () {
//            if (window.console) window.console.log('exitFullscreen');
//        });

        function MapX(X) {
            var mx = parseInt(X);
            mx = mx * 12.8 / 1280-6.4;
            return mx;
        }

        function MapY(Y) {
            var my = parseInt(Y);
            my = 4.95 - my * 9.9 / 990 ;
            return my;
        }


        
        function ReloadData() {

            function OnRequestReady(data) {

                $.each(data.data, function (i, v) {
                    var ok = false;
                    var rect;
                    var j;
                    for (j = 0; j < stanki.length; j++) {
                        if (stanki[j].tag == v.INVN) {
                            ok = true;
                            rect = stanki[j];
                            break;
                        }
                    }



                    var rgb = '#FFFFFF';
                    var rgb1 = new String(v.RGB);
                    rgb1 = '#'+rgb1.trim();

                    var blink = false;
                    if (v.SO == 4 || v.SO == 5)
                        blink = true;

                    if (v.SO == -1 || v.SA == -1) {
                        rgb = '#FFFFFF';
                    } else {
                        switch (v.SO) {
                            case 100:
                                rgb = '#FF0000'; //red
                                break;
                            case 9:
                                rgb = '#FFA500'; //orange
                                break;

                            default:
                                switch (v.SA) {
                                    case 100:
                                        rgb = '#FF0000'; // Red
                                        break;
                                    case 0:
                                        rgb = '#0000FF'; // blue
                                        break;
                                    case 1:
                                        rgb = '#000000'; //black
                                        break;
                                    case 2:
                                        rgb = '#00FF00'; // green
                                    case 3:
                                        rgb = '#00FF00'; //green
                                        break;
                                    default:
                                        rgb = '#909090'; // gray
                                        break;
                                }
                                break;
                        }
                    }



                    var tip = v.INVN + ' (' + v.NAIM + ')  <br/>Изд.: ' + v.IZD + '<br/>Оператор: ' + v.STATUSTEXT + '<br/>Автоматика: ' + v.SATEXT; //+ '[' + v.LOCATIONX + '/' + v.LOCATIONY + ']'
                    if (ok) {
                        rect.setStyle({ color: rgb, opacity: 1.0, fillOpacity: 0.5, weight: 2 });
                        rect.bindPopup(tip);
                        rect.marker.bindPopup(tip);
                        rect.blink = blink;
                    } else {
                        var bounds;
                        var mpos;
                        if (v.ANGLE == '-90') {
                            mpos = [MapY(parseInt(v.LOCATIONY) - parseInt(v.WIDTH) / 2), MapX(parseInt(v.LOCATIONX) + parseInt(v.HEIGHT) / 2)];
                            bounds = [mpos, [MapY(parseInt(v.LOCATIONY) + parseInt(v.WIDTH) / 2), MapX(parseInt(v.LOCATIONX) - parseInt(v.HEIGHT) / 2)]];
                        } else {
                            mpos = [MapY(parseInt(v.LOCATIONY) - parseInt(v.HEIGHT) / 2), MapX(parseInt(v.LOCATIONX) + parseInt(v.WIDTH) / 2)]
                            bounds = [mpos, [MapY(parseInt(v.LOCATIONY) + parseInt(v.HEIGHT) / 2), MapX(parseInt(v.LOCATIONX) - parseInt(v.WIDTH) / 2)]];
                        }
                        rect = L.rectangle(bounds, { color: rgb, opacity: 1.0, fillOpacity: 0.5, weight: 2 });
                        rect.tag = v.INVN;
                        rect.bindPopup(tip);
                        rect.blink = blink;
                        rect.addTo(map);
                        var marker = new L.Marker.Text([MapY(parseInt(v.LOCATIONY)), MapX(parseInt(v.LOCATIONX))], v.INVN, { opacity: 0 }).bindPopup(tip).addTo(map);
                        rect.marker = marker;
                        stanki.push(rect);
                    }
                });


            };
            $.ajax({
                dataType: "json",
                url: 'oborxy.aspx',
                data: 'C=<%=GetC()%>',
                success: OnRequestReady
            });
        }
        var blinkMode = false;
        function Blinker() {
            blinkMode = !blinkMode;


            var j;
            for (j = 0; j < stanki.length; j++) {

                rect = stanki[j];
                if (rect.blink == true) {
                    if (blinkMode) {
                        rect.setStyle({  fillOpacity: 0.1 });
                    } else {
                        rect.setStyle({  fillOpacity: 0.6 });
                    }
                }
               
            }
           

        }

        ReloadData();

       var intervalID = window.setInterval(ReloadData, 60000);

       var intervalID2 = window.setInterval(Blinker, 600);
        </script>
    </form>
</body>
</html>
