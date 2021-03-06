﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function removeStudentAndAllData(){

    var studentID = document.getElementById("selectedStudent").value;

    var jsonObject = {};

    jsonObject.studentID = studentID;

    console.log(JSON.stringify(jsonObject));

    $.ajax({
        url: "/StudentManagment/RemoveStudent",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(response.responseText);
        }
    });

}

function getJsonDataFromPractice() {

    // Loop through grabbing everything
    var practiceData = [];
    var $headers = $("th");
    var $rows = $("tbody tr").each(function (index) {
        $cells = $(this).find("td");
        practiceData[index] = {};
        $cells.each(function (cellIndex) {
            practiceData[index][$($headers[cellIndex]).html()] = $(this).html();
        });
    });

    var courseName = document.getElementById("courseName").innerText;
    var practiceDate = document.getElementById("practiceDate").innerText;
    var teacherAdvice = document.getElementById("teacherAdvice").innerText;
   

    //alert(practiceInfo);

    // Let's put this in the object like you want and convert to JSON (Note: jQuery will also do this for you on the Ajax request)
    var jsonObject = {};
    jsonObject.practiceData = practiceData;
    jsonObject.courseName = courseName;
    jsonObject.practiceDate = practiceDate;
    jsonObject.teacherAdvice = teacherAdvice;

    // Get the form data with our (yet to be defined) function.


    console.log(JSON.stringify(jsonObject));

    $.ajax({
        url: "/PracticeManagment/AddPractice",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });

}

function getJsonDataFromExam() {

    // Loop through grabbing everything
    var examData = [];
    var $headers = $("th");
    var $rows = $("tbody tr").each(function (index) {
        $cells = $(this).find("td");
        examData[index] = {};
        $cells.each(function (cellIndex) {
            examData[index][$($headers[cellIndex]).html()] = $(this).html();
        });
    });

    var courseName = document.getElementById("courseName").innerText;
    var finalGrade = document.getElementById("finalGrade").innerText;
    var examType = document.getElementById("examType").innerText;
    var examDate = document.getElementById("examDate").innerText;
    var teacherAdvice = document.getElementById("teacherAdvice").innerText;
        
    // Let's put this in the object like you want and convert to JSON (Note: jQuery will also do this for you on the Ajax request)
    var jsonObject = {};
    jsonObject.examData = examData;
    jsonObject.courseName = courseName;
    jsonObject.finalGrade = finalGrade;
    jsonObject.examType = examType;
    jsonObject.examDate = examDate;
    jsonObject.teacherAdvice = teacherAdvice;

    // Get the form data with our (yet to be defined) function.
    

    console.log(JSON.stringify(jsonObject));

    $.ajax({
        url: "/ExamManagment/AddExam",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });

}

function getJsonDataFromNotebook() {

    // Loop through grabbing everything
    var notebookData = [];
    var $headers = $("th");
    var $rows = $("tbody tr").each(function (index) {
        $cells = $(this).find("td");
        notebookData[index] = {};
        $cells.each(function (cellIndex) {
            notebookData[index][$($headers[cellIndex]).html()] = $(this).html();
        });
    });

    var notebookDate = document.getElementById("notebookDate").innerText;
    var numberOfDays = document.getElementById("numberOfDays").innerText;
   
    // Let's put this in the object like you want and convert to JSON (Note: jQuery will also do this for you on the Ajax request)
    var jsonObject = {};
    jsonObject.notebookData = notebookData;
    jsonObject.notebookDate = notebookDate;
    jsonObject.numberOfDays = numberOfDays;

    // Get the form data with our (yet to be defined) function.


    console.log(JSON.stringify(jsonObject));

    $.ajax({
        url: "/NotebookManagment/AddNotebook",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });

}

function sendJsonDataForAbsent() {

    var problem = document.getElementById("problem").value;
    var fromDate = document.getElementById("fromDate").value;
    var toDate = document.getElementById("toDate").value;
    var isCertificate = document.getElementById("isCertificate").checked;
    var isTrue = document.getElementById("isTrue").checked;
    var studentID = document.getElementById("selectedStudent").value;

    var jsonObject = {};

    jsonObject.studentID = studentID;
    jsonObject.problem = problem;
    jsonObject.fromDate = fromDate;
    jsonObject.toDate = toDate;
    jsonObject.isCertificate = isCertificate;
    jsonObject.isTrue = isTrue;

    console.log(JSON.stringify(jsonObject));


    $.ajax({
        url: "/AbsentManagment/AddAbsent",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });
}

function sendJsonDataForDefect() {

    var defectType = document.getElementById("defectType").value;
    var defectDescription = document.getElementById("defectDescription").value;
    var defectDate = document.getElementById("defectDate").value;
    var studentID = document.getElementById("selectedStudent").value;

    var jsonObject = {};

    jsonObject.studentID = studentID;
    jsonObject.defectType = defectType;
    jsonObject.defectDescription = defectDescription;
    jsonObject.defectDate = defectDate;

    console.log(JSON.stringify(jsonObject));


    $.ajax({
        url: "/DefectManagment/AddDefect",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });
}

function sendJsonDataForLate() {

    var studentID = document.getElementById("selectedStudent").value;
    var lateDate = document.getElementById("lateDate").value;
    var howMuch = document.getElementById("howMuch").value;
    var problem = document.getElementById("problem").value;
    var isTrue = document.getElementById("isTrue").checked;

    var jsonObject = {};

    jsonObject.studentID = studentID;
    jsonObject.lateDate = lateDate;
    jsonObject.howMuch = howMuch;
    jsonObject.problem = problem;
    jsonObject.isTrue = isTrue;

    console.log(JSON.stringify(jsonObject));


    $.ajax({
        url: "/LateManagment/AddLate",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });
}


function sendJsonDataForJob() {

    var studentID = document.getElementById("studentID").value;
    var cycle = document.getElementById("cycle").value;
    var jobType = document.getElementById("jobType").value;
    var grade = document.getElementById("grade").value;

    var jsonObject = {};

    jsonObject.studentID = studentID;
    jsonObject.cycle = cycle;
    jsonObject.jobType = jobType;
    jsonObject.grade = grade;

    console.log(JSON.stringify(jsonObject));


    $.ajax({
        url: "/JobManagment/AddJob",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            alert(JSON.stringify(jsonObject));
        }
    });
}


function changeCycle() {

    var studentID = document.getElementById("studentID").value;

    var jsonObject = {};

    jsonObject.studentID = studentID;

    console.log(JSON.stringify(jsonObject));


    $.ajax({
        url: "/JobManagment/changeCycle",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.responseText);
        },
        success: function (response) {
            if(response.success){ 
                
                console.log(response.responseText);

                var jsonObj = JSON.parse(response.responseText)
                var Cycle = jsonObj["Cycle"];
                
                document.getElementById('cycle').value = Cycle;
                

            }
        }
    });
}

//////////// For Report

function sendJsonDataToReport() {

    var studentID = document.getElementById("selectedStudent").value;
    var selectedMonth  = document.getElementById("month").value;
    var jsonObject = {};

    jsonObject.studentID = studentID;
    jsonObject.selectedMonth = selectedMonth;

    console.log(JSON.stringify(jsonObject));
 

    $.ajax({
        url: "/Report/CreateReport",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response);
        },
        success: function (response) {
            if(response.success){ 
                
                
                console.log(response.responseText);

                var jsonObj = JSON.parse(response.responseText)
                var CourseForReportDtos = jsonObj["CourseForReportDtos"];
                var ReportDtos = jsonObj["ReportDtos"];
                var Exams =  jsonObj["Exams"];
                var GradeOfNotebook = jsonObj["GradeOfNotebook"];
                var Totalpolicy = jsonObj["Totalpolicy"];
                var NoteBookAves = jsonObj["NoteBookAves"];
                var jobsList = jsonObj["jobsList"];
                var jobsNames = jsonObj["jobsNames"];            

                var ExamList = [];  
                var PracticeList = [];

                var examTemp = [];
                
                var noValue = " ";
                var count = 0;

                var checker1 = false;
                var checker2 = false;

                for (i = 0; i < CourseForReportDtos.length; i++) {
                    

                    var cid =  CourseForReportDtos[i].CourseId;

                    for(j = 0; j < ReportDtos.length; j++){

                        if(ReportDtos[j].CourseId == cid){
                            PracticeList = ReportDtos[j];
                            checker1 = true;
                        }
                        
                    }

                    for(j = 0; j < Exams.length; j++){

                        if(Exams[j].CourseID == cid){
                            examTemp.push(Exams[j]);
                            checker2 = true;
                        }
                        
                    }

                    for(m = 0; m < examTemp.length; m++){
                        ExamList[m] = examTemp[m];
                    }

                    examTemp = [];

                    console.log(PracticeList);

                    if(checker1 || checker2){
                        count++;

                    var absentHelper = [];

                    if(ExamList[0] != undefined)
                    {

                        if(ExamList[0].Grade == 1.1){
                            absentHelper[0] = "غ";
                            ExamList[0].Grade = 0;
                        }else{
                            absentHelper[0] = ExamList[0].Grade;
                        }

                    }else{
                        console.log(ExamList[1]);
                    }

                    if(ExamList[1] != undefined)
                    {
                        console.log("not undefined");
                        
                        if(ExamList[1].Grade == 1.1){
                            absentHelper[1] = "غ";
                            ExamList[1].Grade = 0;
                        }else{
                            absentHelper[1] = ExamList[1].Grade;
                        }
                        
                    }else{
                        console.log(ExamList[1]);
                    }

                    var PercentOfWork = '-';
                    var PercentOfClass = '-';
                    var SeeNumbers = '-';                
    
                    if(typeof PracticeList != undefined && typeof PracticeList != null ){
        
                        PercentOfWork = PracticeList.PercentOfWork + "٪";
                        PercentOfClass = PracticeList.PercentOfClass + "٪";
                        SeeNumbers = PracticeList.SeeNumbers + " بار";
                        if(PracticeList.PercentOfWork == undefined){
                            PercentOfWork = "-";
                            PercentOfClass = "-";
                            SeeNumbers = "-";
                        }
                        
                    }
                        
                        $("#showContent").append(
                        

                            "<tr class='second-table-row'>"+

                                "<td class='id'>"+ count +"</td>" +
                                "<td class='course'>"+ CourseForReportDtos[i].CourseName +"</td>" +
                                "<td colspan='2' >"+((typeof PracticeList == 'undefined') ? '-' : PercentOfWork )+"</td>"+
                                "<td colspan='2' >"+((typeof PracticeList == 'undefined') ? '-' : SeeNumbers )+"</td>"+

                                "<td>"+((typeof ExamList[0] == 'undefined') ? '-' : ExamList[0].ExamDate )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? '-' : ExamList[0].FinalGrade )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? '-' : absentHelper[0] )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? '-' : ExamList[0].ExamType )+"</td>"+
                                
                                "<td>"+((typeof ExamList[1] == 'undefined') ? '-' : ExamList[1].ExamDate )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? '-' : ExamList[1].FinalGrade )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? '-' : absentHelper[1] )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? '-' : ExamList[1].ExamType )+"</td>"+
                                

                            "</tr>"+
          
                        "</tbody>"
                        
                    );
                    }


                    checker1 = false;
                    checker2 = false;
                    
                    PracticeList = {};
                        
                    for(t = 0; t < Exams.length; t++){

                        if(typeof ExamList[t] != 'undefined'){
                            ExamList[t] = {};
                        }

                    }

                }

                $('td').html(function(i, html){
                  return html.replace("undefined", '-'); 
                  return html.replace(" ٪", ' '); 
                  return html.replace("  بار", ' '); 
                });

                //console.log(ReportDtos.CourseName[i]);
                
                //Chart Updating

                var labels = ['هفته اول', 'هفته دوم', 'هفته سوم', 'هفته چهارم', 'هفته پنجم'];
                addData(labels,GradeOfNotebook,Totalpolicy,NoteBookAves,jobsList,jobsNames);

                

                console.log("late : ",Totalpolicy.late);
                console.log("absent : ",Totalpolicy.absent);
                console.log("nazm : ",Totalpolicy.naqsEnzebati);
                console.log("elmi : ",Totalpolicy.naqsElmi);
            }
        }
    });
}

function addData(labels,data,Totalpolicy,NoteBookAves,jobsList,jobsNames) {

    for(m = 0; m < data.length; m++){
        notebookForWeek.data.labels[m] = labels[m];
        notebookForWeek.data.datasets[0].data[m] = data[m];  
    }

    for(m = 0; m < NoteBookAves.length; m++){
        notebookForMonth.data.datasets[0].data[m] = NoteBookAves[m];
    }

    var counterJobs = 0;
    
    let jobsLable = " ";
    let jobsValue = 0;

    for (const [key, value] of Object.entries(jobsList)) {
        console.log(key, value);

        switch(key) {
          case "0":
            jobsLable = " ";
            jobsValue = 0;
            break;
          case "1":
            jobsLable = "دوره اول" + ((typeof jobsNames[1] == 'undefined') ? ' ' : "(" + jobsNames[1] + ")" );
            jobsValue = value;
            break;
          case "2":
            jobsLable = "دوره دوم" + ((typeof jobsNames[2] == 'undefined') ? ' ' : "(" + jobsNames[2] + ")" ) ;
            jobsValue = value;
            break;
          case "3":
            jobsLable = "دوره سوم" + ((typeof jobsNames[3] == 'undefined') ? ' ' : "(" + jobsNames[3] + ")" );
            jobsValue = value;
            break;
          case "4":
            jobsLable = "دوره چهارم" + ((typeof jobsNames[4] == 'undefined') ? ' ' : "(" + jobsNames[4] + ")" );
            jobsValue = value;
            break;
          case "5":
            jobsLable = "دوره پنجم" + ((typeof jobsNames[5] == 'undefined') ? ' ' : "(" + jobsNames[5] + ")" );
            jobsValue = value;
            break;
          case "6":
            jobsLable = "دوره ششم" + ((typeof jobsNames[6] == 'undefined') ? ' ' : "(" + jobsNames[6] + ")" );
            jobsValue = value;
            break;
          case "7":
            jobsLable = "دوره هفتم" + ((typeof jobsNames[7] == 'undefined') ? ' ' : "(" + jobsNames[7] + ")" );
            jobsValue = value;
            break;
          case "8":
            jobsLable = "دوره هشتم"+ ((typeof jobsNames[8] == 'undefined') ? ' ' : "(" + jobsNames[8] + ")" );
            jobsValue = value;
            break;
        }
    
        jobs.data.labels[counterJobs] = jobsLable;
        jobs.data.datasets[0].data[counterJobs] = jobsValue; 

        counterJobs++;
    }

    if(Totalpolicy.late == 0){
            didnotDoPolicy.data.datasets[0].data[0] = 1;
    }else{
            didnotDoPolicy.data.datasets[0].data[0] = Totalpolicy.late * -1;
    }

    if(Totalpolicy.absent == 0){
            didnotDoPolicy.data.datasets[0].data[1] = 1;
    }else{
            didnotDoPolicy.data.datasets[0].data[1] = Totalpolicy.absent * -1;
    }

    if(Totalpolicy.nazm == 0){
            didnotDoPolicy.data.datasets[0].data[2] = 1;
    }else{
            didnotDoPolicy.data.datasets[0].data[2] = Totalpolicy.nazm * -1;
    }

    didnotDoPolicy.data.datasets[0].data[3] = Totalpolicy.elmi * -1;
    
    if(Totalpolicy.mailToHome == 0){
            didnotDoPolicy.data.datasets[0].data[4] = 1;
    }else{
            didnotDoPolicy.data.datasets[0].data[4] = Totalpolicy.mailToHome * -1;
    }

    
    didnotDoPolicy.update();
    notebookForMonth.update();
    notebookForWeek.update();
    jobs.update();
}

/*function ul(index) {
    console.log('click!' + index)

    var underlines = document.querySelectorAll(".underline");

    for (var i = 0; i < underlines.length; i++) {
        underlines[i].style.transform = 'translate3d(' + index * -100 + '%,0,0)';
    }
}*/

var notebookForWeek = new Chart(document.getElementById("line-chart"), {
    type: 'line',
    data: {
        labels: ["هفته اول","هفته دوم","هفته سوم","هفته چهارم","هفته پنجم"],
        datasets: [{
            data: [0,0,0,0,0],
            borderColor: "#22de84",
            fill: true
        }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            fontSize: 25,
            FontFamily:'tanha',
            text: 'نمودار دفترچه راهنما'
        },
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    max: 18
                }
            }]
        }
    }

});

var notebookForMonth = new Chart(document.getElementById("line-chart1"), {
    type: 'line',
    data: {
        labels: ["مهر","ابان", "آذر", "دی", "بهمن", "اسفند", "فروردین", "اردیبهشت","خرداد"],
        datasets: [{
            data: [],
            borderColor: "#22de84",
            fill: true
        }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            fontSize: 25,
            FontFamily:'tanha',
            text: ' نمودار ماهانه دفترچه راهنما '
        },
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    max: 18
                }
            }]
        }
    }
});

/*Chart.types.Line.extend({
  name: "LineAlt",
  draw: function(){
    Chart.types.Line.prototype.draw.apply(this, arguments);

    this.chart.ctx.textAlign = "center"
    // y value and x index
    this.chart.ctx.fillText("ZONE1", this.scale.calculateX(3.5), this.scale.calculateY(20.75))
    this.chart.ctx.fillText("ZONE2", this.scale.calculateX(11.5), this.scale.calculateY(13))
    this.chart.ctx.fillText("ZONE3", this.scale.calculateX(2), this.scale.calculateY(9.75))
    this.chart.ctx.fillText("ZONE4", this.scale.calculateX(14.5), this.scale.calculateY(22.75))
  }
});*/

var barChart = document.getElementById("bar-chart");

var plugin = {
    afterDraw: function (chart) {
        var width = barChart.width,
            height = barChart.height,
            ctx = barChart.getContext('2d');
        ctx.restore();
        ctx.font = "1.4em tanha";
        ctx.textAlign = "center";
        ctx.textBaseline = "middle";
        ctx.fillText("خوب", width * .50, height * .165);
        ctx.fillText("نیازمند تلاش بیشتر", width * .50, height * .257);
        ctx.fillText("پرسش برانگیز", width * .50, height * .340);
        ctx.save();

        
    }
};

Chart.plugins.register(plugin);

Chart.pluginService.register({
    afterDraw: function(chart) {
        if (typeof chart.config.options.lineAt != 'undefined') {


        	var lineAt = chart.config.options.lineAt;
            var ctxPlugin = chart.chart.ctx;
            var xAxe = chart.scales[chart.config.options.scales.xAxes[0].id];
            var yAxe = chart.scales[chart.config.options.scales.yAxes[0].id];
           	
            var holder = 0;
            // I'm not good at maths
            // So I couldn't find a way to make it work ...
            // ... without having the `min` property set to 0
            //if(yAxe.min != 0) return;
            
            ctxPlugin.strokeStyle = "black";
        	ctxPlugin.beginPath();
            //holder = (lineAt[0] - (yAxe.min)) * (100 / (yAxe.max));
            //holder = (100 - holder) / 100 * (yAxe.height) + yAxe.top;
            
            //ctxPlugin.moveTo(xAxe.left, 50);
            //ctxPlugin.lineTo(xAxe.right, 50);
            
            ctxPlugin.moveTo(xAxe.left, 88);
            ctxPlugin.lineTo(xAxe.right, 88);

            //ctxPlugin.moveTo(xAxe.left, 126);
            //ctxPlugin.lineTo(xAxe.right,126);

            //ctxPlugin.moveTo(xAxe.left, 392);
            //ctxPlugin.lineTo(xAxe.right,392);
            
            ctxPlugin.stroke();

        }
    }
});

var didnotDoPolicy = new Chart(barChart, {
    type: 'bar',
    data: {
        labels: ["تاخیر ورود به مدرسه","غیبت غیرموجه","رعایت نظم", "نقص علمی","ارائه نامه خانه"],
        datasets: [
            {
                backgroundColor: '#898585',
                data: [0,0,0,0,0]
            }
        ]
    },
    options: {
        plugins: [plugin],
        legend: { display: false },
        title: {
            display: true,
            fontSize: 25,
            FontFamily:'tanha',
            text: ' نمودار عملکرد علمی انضباطی '
        },
        lineAt: [1,0],
        scales: {
            xAxes: [{
                barPercentage: 0.4,
                gridLines: {
                    display:false
                },
                ticks: {
                    fontSize: 16,
                    FontFamily:'tanha',
                }
            }],
            yAxes: [{
                gridLines: {
                    display:true
                },
                display: true,
                scaleLabel: {
                    display: false,
                    labelString: 'پرسش برانگیز',
                    fontSize:20,
                    FontFamily:'tanha',
                    lineHeight:1
                },
                ticks: {
                    fontSize: 22,
                    min: -8,
                    max: 1,
                    stepSize: 1,
                    callback: function(label){
                        switch(label){

                            case 1:
                                return "۱";
                            case 0:
                                return "۰";
                            case -1:
                                return "۱-";
                            case -2:
                                return "۲-";
                            case -3:
                                return "۳-";
                            case -4:
                                return "۴-";
                            case -5:
                                return "۵-";
                            case -6:
                                return "۶-";
                            case -7:
                                return "۷-";
                            case -8:
                                return "۸-";
                            
                        }
                    }
                }
            }]
        }
    }
});


var jobs = new Chart(document.getElementById("bar-chart-jobs"), {
    type: 'bar',
    data: {
        labels: [],
        datasets: [
            {
                backgroundColor: '#fff',
                borderColor: '#000',
                borderWidth: 5,
                data: []
            }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            fontSize: 25,
            FontFamily:'tanha',
            text: ' نمودار مسئولیت ها '
        },
        scales: {
            xAxes: [{
                barPercentage: 0.4,
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    steps: 1,
                    max: 3,
                    callback: function(label) {
                        switch (label) {
                            case 0:
                                return ' ';
                            case 1:
                                return 'نیازمند تلاش بیشتر';
                            case 2:
                                return 'درحدانتظار';
                            case 3:
                                return 'رضایت بخش';
                        }
                    }
                }
            }]
        }
    }
});

function autoPrint(){

    var x = document.getElementById("selectedStudent").value;
    console.log(x);

    var selectedStudent = x-1;//x-2 after 36

    if(x <= 36){
        selectedStudent = x-1;//x-2 after 36
    }else {
        selectedStudent = x-2;
    }

    var v = document.getElementById("search-report").options.item(selectedStudent).innerText;
    
    var n = v.split(" ");
    console.log(n[n.length - 1]);//last character . class name

    var name = " ";
    for (i = 0; i < n.length - 1; i++) {
        name += n[i] + " ";
    }

    var spaceCount = (v.split(" ").length - 1);
    console.log(spaceCount);

    var t = document.getElementById("nameOfStudent").innerText = "کارنامه ماهیانه" + name + "کلاس هفتم " + n[n.length - 1];
    console.log(t);

}

function tableprint(){

    var d = document.getElementById("charPrint");
    d.className += " dont-print";

    var m = document.getElementById("tblPrint");
    m.classList.remove("dont-print");
    
}

function chartprint(){

    var d = document.getElementById("tblPrint");
    d.className += " dont-print";

    var m = document.getElementById("charPrint");
    m.classList.remove("dont-print");
    
}



//Edit Area -------------------------------------

var courseIdsForUpdate = new Array();
var studentIdsForUpdate = new Array();

var typeOfShowG = "";
 
function showTable() {

    var typeOfShow = document.getElementById("type").value;
    var course  = document.getElementById("course").value;
    var date = document.getElementById("date").value;
    var calssname  = document.getElementById("class").value;

    typeOfShowG = typeOfShow;
    examDateForUpdate = date;    

    var jsonObject = {};

    jsonObject.course = course;
    jsonObject.date = date;
    jsonObject.calssname = calssname;

    console.log(JSON.stringify(jsonObject));
    
    var redirectionAddress = "";    

    if(typeOfShow == "تکلیف"){
        redirectionAddress = "/Edit/showPractice";
    }else if(typeOfShow == "امتحان"){
        redirectionAddress = "/Edit/showExam";
    }else if(typeOfShow == "دفترچه"){
        redirectionAddress = "/Edit/showNotebook";
    }

    $.ajax({
        url: redirectionAddress,
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response);
        },
        success: function (response) {
            console.log(response.responseText);
            
            var jsonObj = JSON.parse(response.responseText);
            console.log(jsonObj);

                console.log(Object.keys(jsonObj[0].Students));
                //console.log(jsonObj[0].Exams[j]);

                var keys = Object.keys(jsonObj[0].Students);
                var values = Object.values(jsonObj[0].Students);
                
                if(typeOfShow == "تکلیف"){
                    var grades = Object.values(jsonObj[0].Practices);
                }else if(typeOfShow == "امتحان"){
                    var grades = Object.values(jsonObj[0].Exams);
                }else if(typeOfShow == "دفترچه"){
                    var grades = Object.values(jsonObj[0].Notebooks);

                    var frameArray = [];
                    for(var i = 0; i < keys.length; i++){
                        frameArray.push(keys[i]);
                    }   

                    studentIdsForUpdate = frameArray; 

                }

                if(typeOfShow != "دفترچه"){

                    console.log(jsonObj[0].CourseIds.length);

                    //new Array for Global for local []
                    var frameArray = [];
                    for(var i = 0; i < jsonObj[0].CourseIds.length; i++){
                        frameArray.push(jsonObj[0].CourseIds[i]);
                    }   

                    courseIdsForUpdate = frameArray;            
                }

                $("#myTable tr").remove(); 

                var table = document.getElementById("myTable");

                var row = table.insertRow(0);
                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);
                var cell3 = row.insertCell(2);

                cell1.innerHTML = "شناسه",
                cell2.innerHTML = "نام و نام خانوادگی",
                cell3.innerHTML = "نمره";

                for (var j = 0; j < keys.length; j++){

                    var row = table.insertRow(j+1);
                    var cell1 = row.insertCell(0);
                    var cell2 = row.insertCell(1);
                    var cell3 = row.insertCell(2);

                    cell1.innerHTML = keys[j],
                    cell2.innerHTML = values[j],
                    cell3.innerHTML = grades[j];

                    cell3.tabIndex = j+1;
                    
                }  

                //$("#myTable").html(table);
            
                document.getElementById("showdate").value = date;

                if(typeOfShow == "تکلیف"){
                    document.getElementById("finalGrade").value = jsonObj[0].Numbers;
                }else if(typeOfShow == "امتحان"){
                    document.getElementById("finalGrade").value = jsonObj[0].FinalGrade;
                }

                document.getElementById("myTable").contentEditable = "true";
       
        }
    });
//location.reload(true);
}


function changeShowPlan(){
    var type  = document.getElementById("type").value; 
    if(type == "دفترچه"){
        document.getElementById("course").style.display = 'none';
        document.getElementById("finalGrade").style.display = 'none';
    }else{
        document.getElementById("course").style.display = 'inline-block';
        document.getElementById("finalGrade").style.display = 'inline-block';
    }
}

function getTableColumnValues(col){
    var columnValues=[];
    $('table[id]').each(function() {
        $('tr>td:nth-child('+col+')',$(this)).each(function(key,value) {
            if(key != 0){//for countinue when read header
                columnValues.push($(this).text());
            }
        });
    });
    return columnValues;
}

function updateDatabase(){

    var examDateForUpdate = document.getElementById("showdate").value;
    var finalGradeForUpdate = document.getElementById("finalGrade").value;

    var Grades = getTableColumnValues(3);

    var jsonObject = {};
    
    if(typeOfShowG == "دفترچه"){
        jsonObject.studentIdsForUpdate = studentIdsForUpdate;
        jsonObject.Grades = Grades;
        jsonObject.examDateForUpdate = examDateForUpdate;
    }else{
        jsonObject.courseIdsForUpdate = courseIdsForUpdate;
        jsonObject.Grades = Grades;
        jsonObject.examDateForUpdate = examDateForUpdate;
    }
    
    console.log(JSON.stringify(jsonObject));

    var redirectionAddress = "";    

    if(typeOfShowG == "تکلیف"){
        redirectionAddress = "/Edit/updatePractice";
    }else if(typeOfShowG == "امتحان"){
        redirectionAddress = "/Edit/updateExam";
    }else if(typeOfShowG == "دفترچه"){
        redirectionAddress = "/Edit/updateNotebook";
    }

    $.ajax({
        url: redirectionAddress,
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response);
        },
        success: function (response) {
            alert(response.responseText);
        }
    });
}     