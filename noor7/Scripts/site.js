// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
    var isTrue = document.getElementById("isTrue").value;

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




//////////// For Report

function sendJsonDataToReport() {

    var studentID = document.getElementById("selectedStudent").value;
    var selectedDate  = document.getElementById("selectedStudent").value;
    var jsonObject = {};

    jsonObject.studentID = studentID;
    //jsonObject.selectedDate = selectedDate;

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
                        
                        
                        $("#showContent").append(
                        "<tbody>"+

                            "<tr class='first-table-row'>" +
                                "<td rowspan='4' class='id'>"+ count +"</td>" +
                                "<td colspan='4' class='course' rowspan='2'>"+ CourseForReportDtos[i].CourseName +"</td>" +
                                "<td colspan='2'>انجام تکالیف</td>"+
                                "<td colspan='2'>دفعات بازدید</td>"+
                                "<td colspan='2'>توصیه شده</td>"+
                            "</tr>"+

                            "<tr>"+
                                "<td colspan='2'>"+((typeof PracticeList == 'undefined') ? ' ' : PracticeList.PercentOfWork )+"</td>"+
                                "<td colspan='2'>"+((typeof PracticeList == 'undefined') ? ' ' : PracticeList.SeeNumbers )+"</td>"+
                                "<td colspan='2'>"+((typeof PracticeList == 'undefined') ? ' ' : " " )+"</td>"+
                            "</tr>"+

                            "<tr class='exam-status-title'>"+

                                "<td class='exam-status1'>"+
                                    "<span>بارم</span>"+
                                    "<span class='exam-type'>"+((typeof ExamList[0] == 'undefined') ? ' ' : " " )+"</span>"+
                                "</td>"+
                                "<td><span>نمره</span></td>"+
                                "<td><span>توصیه شده</span></td>"+
                                "<td><span>نوع</span></td>"+
                                "<td><span>تاریخ</span></td>"+

                                "<td class='exam-status1'>"+
                                    "<span>بارم</span>"+
                                    "<span class='exam-type'>"+((typeof ExamList[1] == 'undefined') ? ' ' : " " )+"</span>"+
                                "</td>"+
                                "<td><span>نمره</span></td>"+
                                "<td><span>توصیه شده</span></td>"+
                                "<td><span>نوع</span></td>"+
                                "<td><span>تاریخ</span></td>"+
                            

                            "</tr>"+

                            "<tr>"+

                                "<td>"+((typeof ExamList[0] == 'undefined') ? noValue : ExamList[0].FinalGrade )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? noValue : ExamList[0].Grade )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? noValue : " " )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? noValue : " " )+"</td>"+
                                "<td>"+((typeof ExamList[0] == 'undefined') ? noValue : ExamList[0].ExamDate )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? noValue : ExamList[1].FinalGrade )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? noValue : ExamList[1].Grade )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? noValue : " " )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? noValue : " " )+"</td>"+
                                "<td>"+((typeof ExamList[1] == 'undefined') ? noValue : ExamList[1].ExamDate )+"</td>"+

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
                  return html.replace("undefined", ' '); 
                });

                //console.log(ReportDtos.CourseName[i]);
                
                //Chart Updating

                var labels = ['هفته اول', 'هفته دوم', 'هفته سوم', 'هفته چهارم', 'هفته پنجم'];
                addData(labels,GradeOfNotebook,Totalpolicy);

                var e = "-" + Totalpolicy.elmi
                var t = "-" + Totalpolicy.total

                var policy = [e,t];
                console.log(e);
            }
        }
    });
}

function addData(labels,data,Totalpolicy) {

    var sumOfData = 0 ;

    

    for(m = 0; m < data.length; m++){
        notebookForWeek.data.labels[m] = labels[m];
        notebookForWeek.data.datasets[0].data[m] = data[m];

        sumOfData += data[m];
    }

    notebookForMonth.data.datasets[0].data[0] = sumOfData/data.length


    didnotDoPolicy.data.datasets[0].data[1] = Totalpolicy.total * -1;
    didnotDoPolicy.data.datasets[0].data[2] = Totalpolicy.elmi * -1;
    
    didnotDoPolicy.update();
    notebookForMonth.update();
    notebookForWeek.update();
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
        labels: ['هفته اول', 'هفته دوم', 'هفته سوم', 'هفته چهارم', 'هفته پنجم',],
        datasets: [{
            data: [0, 0, 0, 0, 0],
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
        }
    }

});

var notebookForMonth = new Chart(document.getElementById("line-chart1"), {
    type: 'line',
    data: {
        labels: ["مهر", "ابان", "آذر", "دی", "بهمن", "اسفند", "فروردین", "اردیبهشت"],
        datasets: [{
            data: [0],
            borderColor: "#22de84",
            fill: true
        }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            text: 'میانگین دفترچه راهنما'
        }
    }

});

var didnotDoPolicy = new Chart(document.getElementById("bar-chart"), {
    type: 'bar',
    data: {
        labels: [" ","انضباطی", "علمی",""],
        datasets: [
            {
                backgroundColor: ["#fff", "#000","#000",""],
                data: [-12,0,0,0]
            }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            text: 'نمودار نقاص عملکرد علمی - انضباطی'
        }
    }
});

function tableprint(){

    var d = document.getElementById("charPrint");
    d.className += " dont-print";

    var m = document.getElementById("tblPrint");
    m.classList.remove("dont-print");

    var x = document.getElementById("selectedStudent").value;
    console.log(x);

    var selectedStudent = x-1;

    var v = document.getElementById("search-report").options.item(selectedStudent).innerText;
    console.log(v);

    var t = document.getElementById("nameOfStudent").innerText = "کارنامه ماهیانه " + v + " - پایه هفتم";
    console.log(t);

    
}

function chartprint(){

    var d = document.getElementById("tblPrint");
    d.className += " dont-print";


    var m = document.getElementById("charPrint");
    m.classList.remove("dont-print");

    
}