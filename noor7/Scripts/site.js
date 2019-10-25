// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
   

    //alert(practiceInfo);

    // Let's put this in the object like you want and convert to JSON (Note: jQuery will also do this for you on the Ajax request)
    var jsonObject = {};
    jsonObject.practiceData = practiceData;
    jsonObject.courseName = courseName;
    jsonObject.practiceDate = practiceDate;

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


    // Let's put this in the object like you want and convert to JSON (Note: jQuery will also do this for you on the Ajax request)
    var jsonObject = {};
    jsonObject.examData = examData;
    jsonObject.courseName = courseName;
    jsonObject.finalGrade = finalGrade;
    jsonObject.examType = examType;
    jsonObject.examDate = examDate;

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


/*function ul(index) {
    console.log('click!' + index)

    var underlines = document.querySelectorAll(".underline");

    for (var i = 0; i < underlines.length; i++) {
        underlines[i].style.transform = 'translate3d(' + index * -100 + '%,0,0)';
    }
}*/

new Chart(document.getElementById("line-chart"), {
    type: 'line',
    data: {
        labels: ['هفته اول', 'هفته دوم', 'هفته سوم', 'هفته چهارم', 'هفته پنجم',],
        datasets: [{
            data: [12, 8, 5, 9, 3],
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
            text: 'نمودار بازدید تکلیف'
        }
    }

});

new Chart(document.getElementById("line-chart1"), {
    type: 'line',
    data: {
        labels: ["مهر", "ابان", "آذر", "دی", "بهمن", "اسفند", "فروردین", "اردیبهشت"],
        datasets: [{
            data: [12, 2],
            borderColor: "#22de84",
            fill: true
        }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            text: 'میانگین بازدید تکلیف'
        }
    }

});

new Chart(document.getElementById("bar-chart"), {
    type: 'bar',
    data: {
        labels: ["مهر", "ابان", "آذر", "دی", "بهمن", "اسفند", "فروردین", "اردیبهشت"],
        datasets: [
            {
                backgroundColor: ["#eb0c0c", "#eb0c0c", "#eb0c0c", "#eb0c0c", "#eb0c0c", "#eb0c0c", "#eb0c0c", "#eb0c0c", "#eb0c0c"],
                data: [-11, 0, 0, 0, 0, 0, 0, 0]
            }
        ]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            text: 'نمودار رعایت مقررات و وظایف'
        }
    }
});