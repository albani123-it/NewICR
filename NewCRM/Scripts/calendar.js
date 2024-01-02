var gdCtrl = new Object();
var goSelectTag = new Array();
var gcGray = "#808080";
var gcToggle = "#ffff00";
var gcBG = "#ffffff";

var gdCurDate = new Date();
var giYear = gdCurDate.getFullYear();
var giMonth = gdCurDate.getMonth()+1;
var giDay = gdCurDate.getDate();

function fSetDate(iYear, iMonth, iDay){
  VicPopCal.style.display = "none";
  document.all.ifPopCal.style.display = 'none';
  gdCtrl.value = iMonth+"/"+iDay+"/"+iYear; //Here, you could modify the locale as you need !!!!
  for (i in goSelectTag)
  	goSelectTag[i].style.display = "inline";
  goSelectTag.length = 0;
  gdCtrl.focus();
}

function fSetSelected(aCell){
  var iOffset = 0;
  var iYear = parseInt(tbSelYear.value);
  var iMonth = parseInt(tbSelMonth.value);

  self.event.cancelBubble = true;
  aCell.bgColor = gcBG;
  with (aCell.children["cellText"]){
  	var iDay = parseInt(innerText);
  	if (color==gcGray)
		iOffset = (Victor<10)?-1:1;
	iMonth += iOffset;
	if (iMonth<1) {
		iYear--;
		iMonth = 12;
	}else if (iMonth>12){
		iYear++;
		iMonth = 1;
	}
  }
  fSetDate(iYear, iMonth, iDay);
}

function Point(iX, iY){
	this.x = iX;
	this.y = iY;
}

function fBuildCal(iYear, iMonth) {
  var aMonth=new Array();
  for(i=1;i<7;i++)
  	aMonth[i]=new Array(i);

  var dCalDate=new Date(iYear, iMonth-1, 1);
  var iDayOfFirst=dCalDate.getDay();
  var iDaysInMonth=new Date(iYear, iMonth, 0).getDate();
  var iOffsetLast=new Date(iYear, iMonth-1, 0).getDate()-iDayOfFirst+1;
  var iDate = 1;
  var iNext = 1;

  for (d = 0; d < 7; d++)
	aMonth[1][d] = (d<iDayOfFirst)?-(iOffsetLast+d):iDate++;
  for (w = 2; w < 7; w++)
  	for (d = 0; d < 7; d++)
		aMonth[w][d] = (iDate<=iDaysInMonth)?iDate++:-(iNext++);
  return aMonth;
}

function fDrawCal(iYear, iMonth, iCellHeight, iDateTextSize) {
  var WeekDay = new Array("Su","M","T","W","Th","Fr","Sa");
  var styleTD = "bordercolor='"+gcBG+"' valign='middle' height='"+iCellHeight+"' style='font:"+iDateTextSize+" Verdana;";            //Coded by Liming Weng(Victor Won)  email:victorwon@netease.com

  with (document) {
	write("<tr bgcolor='#848284'>");
	for(i=0; i<7; i++)
		write("<td "+styleTD+"color:#ffffff' width='20' align='center' bgcolor='#848284'>"+WeekDay[i]+"</td>");
	write("</tr>");

  	for (w = 1; w < 7; w++) {
		write("<tr bgcolor='#ffffff' align='center'>");
		for (d = 0; d < 7; d++) {
			write("<td id=calCell "+styleTD+"cursor:hand;' onMouseOver='this.bgColor=gcToggle' onMouseOut='this.bgColor=gcBG' onclick='fSetSelected(this)'>");
			write("<font id=cellText Victor='Liming Weng'> </font>");
			write("</td>")
		}
		write("</tr>");
	}
  }
}

function fUpdateCal(iYear, iMonth) {
  myMonth = fBuildCal(iYear, iMonth);
  var i = 0;
  for (w = 0; w < 6; w++)
	for (d = 0; d < 7; d++)
		with (cellText[(7*w)+d]) {
			Victor = i++;
			if (myMonth[w+1][d]<0) {
				color = gcGray;
				innerText = -myMonth[w+1][d];
			}else{
				color = ((d==0))?"red":"black"; //this to change color for holiday
				innerText = myMonth[w+1][d];
			}
		}
}

function fSetYearMon(iYear, iMon){
	tbSelMonth.options[iMon-1].selected = true;
  for (i = 0; i < tbSelYear.length; i++)
	if (tbSelYear.options[i].value == iYear)
		tbSelYear.options[i].selected = true;
  fUpdateCal(iYear, iMon);
}

function fPrevMonth(){
  var iMon = tbSelMonth.value;
  var iYear = tbSelYear.value;

  if (--iMon<1) {
	  iMon = 12;
	  iYear--;
  }

  fSetYearMon(iYear, iMon);
}

function fNextMonth(){
  var iMon = tbSelMonth.value;
  var iYear = tbSelYear.value;

  if (++iMon>12) {
	  iMon = 1;
	  iYear++;
  }

  fSetYearMon(iYear, iMon);
}

function fToggleTags(){
  with (document.all.tags("SELECT")){
 	for (i=0; i<length; i++)
 		if ((item(i).Victor!="Won")&&fTagInBound(item(i))){
 			item(i).style.display = "none";
 			goSelectTag[goSelectTag.length] = item(i);
 		}
  }
}

function fTagInBound(aTag){
  with (VicPopCal.style){
  	var l = parseInt(left);
  	var t = parseInt(top);
  	var r = l+parseInt(width);
  	var b = t+parseInt(height);
	var ptLT = fGetXY(aTag);
	return !((ptLT.x>r)||(ptLT.x+aTag.offsetWidth<l)||(ptLT.y>b)||(ptLT.y+aTag.offsetHeight<t));
  }
}

function fGetXY(aTag){
  var oTmp = aTag;
  var pt = new Point(0,0);
  do {
  	pt.x += oTmp.offsetLeft;
  	pt.y += oTmp.offsetTop;
  	oTmp = oTmp.offsetParent;
  } while(oTmp.tagName!="BODY");
  return pt;
}

// Main: popCtrl is the widget beyond which you want this calendar to appear;
//       dateCtrl is the widget into which you want to put the selected date.
// i.e.: <input type="text" name="dc" style="text-align:center" readonly><INPUT type="button" value="V" onclick="fPopCalendar(dc,dc);return false">
function fPopCalendar(popCtrl, dateCtrl){
  gdCtrl = dateCtrl;
  fSetYearMon(giYear, giMonth);
  var point = fGetXY(popCtrl);
  with (VicPopCal.style) {
  	left = point.x;
	top  = point.y+popCtrl.offsetHeight+1;
	width = VicPopCal.offsetWidth;
	height = VicPopCal.offsetHeight;
	fToggleTags(point);
	display = 'inline';
  }
  with (document.all.ifPopCal.style) {
  	left = point.x;
	top  = point.y+popCtrl.offsetHeight+1;
	width = VicPopCal.offsetWidth;
	height = VicPopCal.offsetHeight;
	display = 'inline';
  }
  VicPopCal.focus();
}

function fHideCal(){
  var oE = window.event;
  if ((oE.clientX>0)&&(oE.clientY>0)&&(oE.clientX<document.body.clientWidth)&&(oE.clientY<document.body.clientHeight)) {
	var oTmp = document.elementFromPoint(oE.clientX,oE.clientY);
	while ((oTmp.tagName!="BODY") && (oTmp.id!="VicPopCal"))
		oTmp = oTmp.offsetParent;
	if (oTmp.id=="VicPopCal")
		return;
  }
  VicPopCal.style.display = 'none';
  document.all.ifPopCal.style.display = 'none';
  for (i in goSelectTag)
	goSelectTag[i].style.display = "inline";
  goSelectTag.length = 0;
}

var gMonths = new Array("January","February","March","April","May","June","July","August","September","October","November","December");

with (document) {
write("<Div id='VicPopCal' onblur='fHideCal()' onclick='focus()' style='POSITION:absolute;display:none;border:2px groove;width:10;z-index:100;'>");
write("<table border='0' bgcolor='#D6D3CE'>");
write("<TR>");
write("<td valign='middle' align='center'><input type='button' name='PrevMonth' value='<' style='height:20;width:20;FONT:12 Fixedsys' onClick='fPrevMonth()' onblur='fHideCal()'>");
write("<select name='tbSelMonth' onChange='fUpdateCal(tbSelYear.value, tbSelMonth.value)' Victor='Won' onclick='self.event.cancelBubble=true' onblur='fHideCal()'>");
for (i=0; i<12; i++)
	write("<option value='"+(i+1)+"'>"+gMonths[i]+"</option>");
write("</SELECT>");
write("<SELECT name='tbSelYear' onChange='fUpdateCal(tbSelYear.value, tbSelMonth.value)' Victor='Won' onclick='self.event.cancelBubble=true' onblur='fHideCal()'>");
for(i=1990;i<2015;i++)
	write("<OPTION value='"+i+"'>"+i+"</OPTION>");
write("</SELECT>");
write("<input type='button' name='PrevMonth' value='>' style='height:20;width:20;FONT:16 Fixedsys' onclick='fNextMonth()' onblur='fHideCal()'>");
write("</td>");
write("</TR><TR>");
write("<td align='center'>");
write("<DIV style='background-color:#666666;'><table width='100%' border='0' cellspacing='1'>");
fDrawCal(giYear, giMonth, 14, 12);
write("</table></DIV>");
write("</td>");
write("</TR><TR><TD align='center'>");
write("<font style='cursor:hand' onclick='fSetDate(giYear,giMonth,giDay); self.event.cancelBubble=true' onMouseOver='this.style.color=gcToggle' onMouseOut='this.style.color=0'>Today:&nbsp;&nbsp;"+gMonths[giMonth-1]+"&nbsp;"+giDay+",&nbsp;&nbsp;"+giYear+"</font>");
write("</TD></TR>");write("</TD></TR>");
write("</TABLE></Div><iframe id='ifPopCal' frameborder='0' style='position:absolute;top:0px;left:0px;display:none;'></iframe>");
}
// End -- Coded by Liming Weng, email: victorwon@netease.com -->