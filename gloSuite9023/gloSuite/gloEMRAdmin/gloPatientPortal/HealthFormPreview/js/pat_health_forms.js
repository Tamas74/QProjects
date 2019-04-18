/// <reference path="Master.js" />

//include('Master.js');
var FocusID = "";
var categoryType = [];
$(document).ready(function () {
    $('#rootwizard').bootstrapWizard({ onTabShow: function (tab, navigation, index) {
        var $total = navigation.find('li').length;
        var $current = index + 1;
        var $percent = ($current / $total) * 100;
        $('#rootwizard').find('.bar').css({ width: $percent + '%' });
    }

    });
    window.prettyPrint && prettyPrint();

    $('#modalValidationMsg').on('hidden', function () {
        if (FocusID !== "") {
            SetFocustoControl(FocusID);
        }
    })
     loaddata();
    //setdata();
    setTimeout('$("html").removeClass("js")', 1000);
    // End
});

function SetFocustoControl(ControlID) {
    try {
        var control = '#' + ControlID
        var divcls = $(control).parents('div[class^="tab-pane"]');
        var tabno = 0;
        if (divcls[0].id !== "") {
            tabno = divcls[0].id.match(/\d+/);
        }
        if (tabno > 0) {
            tabno = parseInt(tabno) - 1;
            $('#rootwizard').bootstrapWizard('show', tabno)
            $(control).focus();
        }

    } catch (e) {
    }
}


function gettabno(ControlID) {
    //var control = '#' + ControlID
    //var divcls = $(control).parents('div[class^="tab-pane"]');  // get Parent (tabID)
    //$('#rootwizard').find('#tab1').show();
    $('#rootwizard').bootstrapWizard('show', 0); // 0 based tab index working
    $('#txt_PatientReportedMedications_Q1').focus();
    //alert(divcls[0].id);
}

function setdata() {
    try {

        ShowProgressbarwithOverlay();
        Type = "POST";
        Url = globalServiceURL + "HelathformData";

        var practid = $.rc4EncryptStr(globalausid, globalkey);

        var formID = sessionStorage.patformid;
        formID = $.rc4EncryptStr(formID, globalkey);

        var npatientid = sessionStorage.LoggedInUserID;
        npatientid = $.rc4EncryptStr(npatientid, globalkey);

        var msg2 = { "SearchRequest": { "appCodeField": globalAppCodeField, "practiceIDField": practid, "vendorIDField": globalVendorID, "SessionId": sessionStorage.PatientPortalSessionID },
            "crt": { "HealthformID": formID, "PatientID": npatientid }
        };

        Data = JSON.stringify(msg2);
        ContentType = "application/json";
        DataType = "json"; ProcessData = true;
        method = "GetHelathformData";

        CallService();

    } catch (e) { HideProgressbarwithOverlay(); }
}

function loaddata() {
    try {
        getgroup();
        getquestion();
        getanswers();
        //getfq();
    } catch (e) {
        //$('#SpanValidaitonMessage').html("Error in Loaddata.");
        //$('#modalValidationMsg').modal();
    }
}

var group = [];
var question = [];
var Answer = [];
var pfselection = [];

var followedQA = [];

function getgroup() {
    $("[controltype=1]").each(function (k) {
        var grp = new Object();
        var cntrlType = this.getAttribute("controltype");
        var grpID = this.getAttribute("GroupId");
        var categoryID = this.getAttribute("HistoryCategoryId");
        var category = this.getAttribute("HistoryCategory");
        var categoryType = this.getAttribute("HistoryCategoryType");
        var isdatatable = "0";
        var grouplabel = "";

        if (this.hasAttribute("GroupLabel")) {
            grouplabel = this.getAttribute("GroupLabel");
        }
        if (this.hasAttribute("datatable")) {
            isdatatable = this.getAttribute("datatable");
        }
        grp = { "controltype": cntrlType, "HistoryCategory": category, "GroupId": grpID, "HistoryCategoryId": categoryID, "HistoryCategoryType": categoryType, "isdatatable": isdatatable, "grouplabel": grouplabel };
        group.push(grp);
    });
}

function getquestion() {
    $("[controltype=2]").each(function (k) {
        var que = new Object();
        var cntrlType = this.getAttribute("controltype");
        var grpID = this.getAttribute("GroupId");
        var queType = this.getAttribute("QuestionType");
        var queID = this.getAttribute("QuestionID");
        var AnswerType = this.getAttribute("AnswerType");
        var categoryID = this.getAttribute("HistoryCategoryId");
        var category = this.getAttribute("HistoryCategory");
        var categoryType = this.getAttribute("HistoryCategoryType");
        var historyItemID = this.getAttribute("HistoryItemId");
        var historyItem = this.getAttribute("historyitem");
        var mandatary = "false";
        var questionlabel = "";
        var isdatatable = "0";

        if (this.hasAttribute("QuestionLabel")) {
            questionlabel = this.getAttribute("QuestionLabel");
        }

        if (this.hasAttribute("datatable")) {
            isdatatable = this.getAttribute("datatable");
        }

        if (this.hasAttribute("Mandatary")) {
            mandatary = this.getAttribute("Mandatary").toLowerCase();
        }

        que = { "controltype": cntrlType, "HistoryCategory": category, "GroupId": grpID, "historyitem": historyItem, "QuestionType": queType, "QuestionID": queID, "HistoryCategoryId": categoryID, "AnswerType": AnswerType, "HistoryItemId": historyItemID, "AnswerIDs": "", "AnswerLables": "", "OtherIDs": "", "HistoryCategoryType": categoryType, "questionlabel": questionlabel, "isdatatable": isdatatable, "mandatary": mandatary };
        question.push(que);
    });
}

function getanswers() {
    $("[controltype=3]").each(function (k) {
        var ans = new Object();
        var cntrlType = this.getAttribute("controltype");
        var grpID = this.getAttribute("GroupId");
        var queID = this.getAttribute("QuestionID");
        var AnswerID = this.getAttribute("AnswerId");
        var historyitemid = this.getAttribute("historyitemid");
        var historyitem = this.getAttribute("historyitem");
        var otherid = this.getAttribute("otherid");
        ans = { "controltype": cntrlType, "GroupId": grpID, "QuestionID": queID, "AnswerId": AnswerID, "historyitemid": historyitemid, "historyitem": historyitem, "OtherID": otherid, "AnswerLable": "" };
        Answer.push(ans);
    });
}

function getPfselection(isdatatable, dtgrpid) {
    var IsValidData = true;
    var AnsId = "";
    var OthID = "";
    var AnswerIDs = "";
    var OtherIDs = "";
    var AnswerLables = "";
    var followedAns = "";
    //pfselection = [];

    for (var i = 0; i < question.length; i++) {
        if (IsValidData === false) {
            break;
        }
        var grpId = question[i].GroupId;
        var queID = question[i].QuestionID;
        var AnsType = question[i].AnswerType;
        var ismandatary = question[i].mandatary;
        var grplabel = getgrouplabel(grpId);
        AnswerIDs = "";
        OtherIDs = "";
        AnswerLables = "";
        followedAns = "";
        if (isdatatable === true) {
            if (dtgrpid !== grpId) {
                continue;
            }
        }
        else {
            if (question[i].isdatatable === "1") {
                continue;
            }
        }
        if (AnsType == 2) { // 2 Checkbox
            var cnt = 0;
            cnt = $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").length;
            var cntattempted = 0;
            var firstcontrolid = "";

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                OthID = this.getAttribute("OtherId");

                var IsFq = "";
                IsFq = this.getAttribute("Isfq");

                $(this).find("input[type=checkbox]").each(function (k) {
                    if (firstcontrolid === "") {
                        firstcontrolid = this.id;
                    }
                    if (this.checked) {
                        var anslbl = this.getAttribute("AnswerLabel").trim();
                        cntattempted += 1;
                        followedAns = "";
                        if (IsFq == "1") {
                            $("[controltype=4][GroupId='" + grpId + "'][QuestionId='" + queID + "'][AnswerId='" + AnsId + "']").each(function (k) {
                                $(this).find("input[type=text]").each(function (k) {
                                    if (this.value.toString() === "") {
                                        FocusID = this.id;
                                        $('#SpanValidaitonMessage').html("Followed Question has to be answered for <b>" + question[i].questionlabel + "</b> (" + anslbl + ") in <b>" + grplabel + "</b> section");
                                        $('#modalValidationMsg').modal();
                                        IsValidData = false;
                                        return IsValidData;
                                    }
                                    else if (this.value.toString().length > 500) {
                                        FocusID = this.id;
                                        $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + question[i].questionlabel + "</b> (" + anslbl + ") in <b>" + grplabel + "</b> section");
                                        $('#modalValidationMsg').modal();
                                        IsValidData = false;
                                        return IsValidData;
                                    }
                                    followedAns = this.value;
                                });
                            });
                        }
                        if (followedAns == "") {
                            AnswerLables += this.getAttribute("AnswerLabel").trim() + ", ";
                        }
                        else {
                            AnswerLables += this.getAttribute("AnswerLabel").trim() + " " + fqstartseperator + followedAns + fqendseperator + ", ";
                        }

                        for (var j = 0; j < Answer.length; j++) {
                            if (question[i].GroupId == Answer[j].GroupId && question[i].QuestionID == Answer[j].QuestionID && AnsId == Answer[j].AnswerId) {
                                if (followedAns == "") {
                                    Answer[j].AnswerLable = this.getAttribute("AnswerLabel").trim();
                                }
                                else {
                                    Answer[j].AnswerLable = this.getAttribute("AnswerLabel").trim() + " " + fqstartseperator + followedAns + fqendseperator;
                                }
                                fillpfselection(0, question[i].GroupId, question[i].QuestionID, Answer[j].AnswerId, this.getAttribute("AnswerLabel").trim(), followedAns);
                            }
                        }

                        AnswerIDs += AnsId + ",";
                        OtherIDs += OthID + ",";

                    }
                });
            });
            if (ismandatary === "true" && cntattempted === 0) {
                FocusID = firstcontrolid;
                $('#SpanValidaitonMessage').html("Question: <b>" + question[i].questionlabel + "</b> is mandatory in <b>" + grplabel + "</b> section");
                $('#modalValidationMsg').modal();
                IsValidData = false;
                return IsValidData;
            }
        }
        else if (AnsType == 3) { // 3 Radio
            var cnt = 0;
            cnt = $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").length;
            var cntattempted = 0;
            var firstcontrolid = "";

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                OthID = this.getAttribute("OtherId");

                var IsFq = "";
                IsFq = this.getAttribute("Isfq");
                $(this).find("input[type=radio]").each(function (k) {
                    if (firstcontrolid === "") {
                        firstcontrolid = this.id;
                    }
                    if (this.checked) {
                        var anslbl = this.getAttribute("AnswerLabel").trim();
                        cntattempted += 1;
                        followedAns = "";
                        if (IsFq == "1") {
                            $("[controltype=4][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                                $(this).find("input[type=text]").each(function (k) {
                                    if (this.value.toString() === "") {
                                        FocusID = this.id;
                                        $('#SpanValidaitonMessage').html("Followed Question has to be answered for <b>" + question[i].questionlabel + "</b> (" + anslbl + ") in <b>" + grplabel + "</b> section");
                                        $('#modalValidationMsg').modal();
                                        IsValidData = false;
                                        return IsValidData;
                                    }
                                    else if (this.value.toString().length > 500) {
                                        FocusID = this.id;
                                        $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + question[i].questionlabel + "</b> (" + anslbl + ") in <b>" + grplabel + "</b> section");
                                        $('#modalValidationMsg').modal();
                                        IsValidData = false;
                                        return IsValidData;
                                    }
                                    followedAns = this.value;
                                });
                            });
                        }
                        if (followedAns == "") {
                            AnswerLables += this.getAttribute("AnswerLabel").trim() + ", ";
                        }
                        else {
                            AnswerLables += this.getAttribute("AnswerLabel").trim() + " " + fqstartseperator + followedAns + fqendseperator + ", ";
                        }

                        for (var j = 0; j < Answer.length; j++) {
                            if (question[i].GroupId == Answer[j].GroupId && question[i].QuestionID == Answer[j].QuestionID && AnsId == Answer[j].AnswerId) {
                                if (followedAns == "") {
                                    Answer[j].AnswerLable = this.getAttribute("AnswerLabel").trim();
                                }
                                else {
                                    Answer[j].AnswerLable = this.getAttribute("AnswerLabel").trim() + " " + fqstartseperator + followedAns + fqendseperator;
                                }
                                fillpfselection(0, question[i].GroupId, question[i].QuestionID, Answer[j].AnswerId, this.getAttribute("AnswerLabel").trim(), followedAns);
                            }
                        }

                        AnswerIDs += AnsId + ",";
                        OtherIDs += OthID + ",";
                    }
                });
            });
            if (ismandatary === "true" && cntattempted === 0) {
                FocusID = firstcontrolid;
                $('#SpanValidaitonMessage').html("Question: <b>" + question[i].questionlabel + "</b> is mandatory in <b>" + grplabel + "</b> section");
                $('#modalValidationMsg').modal();
                IsValidData = false;
                return IsValidData;
            }
        }
        else if (AnsType == 0) { // text
            var cnt = 0;
            cnt = $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").length;
            var cntattempted = 0;
            var firstcontrolid = "";

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {

                AnsId = this.getAttribute("AnswerId");
                OthID = this.getAttribute("OtherId");
                if (AnsId === null) {
                    AnsId = "0";
                }
                if (OthID === null) {
                    OthID = "0";
                }
                $(this).find("input[type=text]").each(function (k) {
                    if (firstcontrolid === "") {
                        firstcontrolid = this.id;
                    }
                    if (this.value.toString().trim() != "") {
                        cntattempted += 1;
                        if (this.value.toString().length > 500) {
                            FocusID = this.id;
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + question[i].questionlabel + "</b> in <b>" + grplabel + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValidData = false;
                            return IsValidData;
                        }

                        fillpfselection(0, question[i].GroupId, question[i].QuestionID, 0, this.value.toString().trim(), "");
                        //                        AnswerLables += this.value.toString().trim() + ", ";
                        AnswerLables += fqstartseperator + this.value.toString().trim() + fqendseperator + ", ";
                        AnswerIDs += AnsId + ",";
                        OtherIDs += OthID + ",";
                    }
                });
            });
            if (ismandatary === "true" && cntattempted === 0) {
                FocusID = firstcontrolid;
                $('#SpanValidaitonMessage').html("Question: <b>" + question[i].questionlabel + "</b> is mandatory in <b>" + grplabel + "</b> section");
                $('#modalValidationMsg').modal();
                IsValidData = false;
                return IsValidData;
            }
        }
        else if (AnsType == 1) { // textArea
            var cnt = 0;
            cnt = $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").length;
            var cntattempted = 0;
            var firstcontrolid = "";

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                OthID = this.getAttribute("OtherId");
                if (AnsId === null) {
                    AnsId = "0";
                }
                if (OthID === null) {
                    OthID = "0";
                }
                $(this).find("textarea").each(function (k) {

                    if (firstcontrolid === "") {
                        firstcontrolid = this.id;
                    }
                    if (this.value.toString().trim() != "") {
                        cntattempted += 1;
                        if (this.value.toString().length > 500) {
                            FocusID = this.id;
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + question[i].questionlabel + "</b> in <b>" + grplabel + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValidData = false;
                            return IsValidData;
                        }
                        fillpfselection(0, question[i].GroupId, question[i].QuestionID, 0, this.value.toString().trim(), "");
                        //  AnswerLables += this.value.toString().trim() + ", ";
                        AnswerLables += fqstartseperator + this.value.toString().trim() + fqendseperator + ", ";
                        AnswerIDs += AnsId + ",";
                        OtherIDs += OthID + ",";
                    }
                });
            });

            if (ismandatary === "true" && cntattempted === 0) {
                FocusID = firstcontrolid;
                $('#SpanValidaitonMessage').html("Question: <b>" + question[i].questionlabel + "</b> is mandatory in <b>" + grplabel + "</b> section");
                $('#modalValidationMsg').modal();
                IsValidData = false;
                return IsValidData;
            }
        }
        else if (AnsType == 4) { // Dropdown
            var cnt = 0;
            cnt = $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").length;
            var cntattempted = 0;
            var firstcontrolid = "";


            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                OthID = this.getAttribute("OtherId");
                var IsFq = "";
                followedAns = "";
                IsFq = this.getAttribute("Isfq");
                if (firstcontrolid === "") {
                    firstcontrolid = $(this).parent()[0].id;
                }
                if (this.selected) {
                    cntattempted += 1;
                    if (IsFq == "1") {
                        $("[controltype=4][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                            $(this).find("input[type=text]").each(function (k) {
                                if (this.value.toString() === "") {
                                    FocusID = this.id;
                                    $('#SpanValidaitonMessage').html("Followed Question has to be answered for <b>" + question[i].questionlabel + "</b> in <b>" + grplabel + "</b> section");
                                    $('#modalValidationMsg').modal();
                                    IsValidData = false;
                                    return IsValidData;
                                }
                                else if (this.value.toString().length > 500) {
                                    FocusID = this.id;
                                    $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + question[i].questionlabel + "</b> in <b>" + grplabel + "</b> section");
                                    $('#modalValidationMsg').modal();
                                    IsValidData = false;
                                    return IsValidData;
                                }
                                followedAns = this.value;
                            });
                        });
                    }
                    if (followedAns == "") {
                        AnswerLables += this.getAttribute("AnswerLabel").trim() + ", ";
                    }
                    else {
                        AnswerLables += this.getAttribute("AnswerLabel").trim() + " " + fqstartseperator + followedAns + fqendseperator + ", ";
                    }

                    for (var j = 0; j < Answer.length; j++) {
                        if (question[i].GroupId == Answer[j].GroupId && question[i].QuestionID == Answer[j].QuestionID && AnsId == Answer[j].AnswerId) {
                            if (followedAns == "") {
                                Answer[j].AnswerLable = this.getAttribute("AnswerLabel").trim();
                            }
                            else {
                                Answer[j].AnswerLable = this.getAttribute("AnswerLabel").trim() + " " + fqstartseperator + followedAns + fqendseperator;
                            }
                            fillpfselection(0, question[i].GroupId, question[i].QuestionID, Answer[j].AnswerId, this.getAttribute("AnswerLabel").trim(), followedAns);
                        }
                    }
                    AnswerIDs += AnsId + ",";
                    OtherIDs += OthID + ",";
                }
            });
            if (ismandatary === "true" && cntattempted === 0) {
                FocusID = firstcontrolid;
                $('#SpanValidaitonMessage').html("Question: <b>" + question[i].questionlabel + "</b> is mandatory in <b>" + grplabel + "</b> section");
                $('#modalValidationMsg').modal();
                IsValidData = false;
                return IsValidData;
            }

        }
    }
    return IsValidData;
    //Submitdata();
}



// Resetcontrol

function resetcontrols(dtID, collectdata) {

    var grpdivid = $('#' + dtID)[0].getAttribute("parentid");
    var dtgrpid = $('#' + grpdivid)[0].getAttribute("GroupId");

    var IsValidData = true;
    var AnsId = "";
    //pfselection = [];
    for (var i = 0; i < question.length; i++) {
        if (IsValidData === false) {
            break;
        }
        var grpId = question[i].GroupId;
        var queID = question[i].QuestionID;
        var AnsType = question[i].AnswerType;
        AnswerIDs = "";
        OtherIDs = "";
        AnswerLables = "";
        followedAns = "";

        if (dtgrpid !== grpId) {
            continue;
        }


        //        if (isdatatable === true) {
        //           
        //        }
        //        else {
        //            if (question[i].isdatatable === "1") {
        //                continue;
        //            }
        //        }
        if (AnsType == 2) { // 2 Checkbox

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                var IsFq = "";
                IsFq = this.getAttribute("Isfq");

                $(this).find("input[type=checkbox]").each(function (k) {

                    $('#' + $(this)[0].id).prop('checked', false).triggerHandler('click');
                    if (IsFq == "1") {
                        $("[controltype=4][GroupId='" + grpId + "'][QuestionId='" + queID + "'][AnswerId='" + AnsId + "']").each(function (k) {
                            $(this).find("input[type=text]").each(function (k) {
                                this.value = "";

                            });
                        });
                    }

                });
            });
        }
        else if (AnsType == 3) { // 3 Radio
            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                var IsFq = "";
                IsFq = this.getAttribute("Isfq");
                $(this).find("input[type=radio]").each(function (k) {
                    $('#' + $(this)[0].id).prop('checked', false).triggerHandler('click');
                    if (IsFq == "1") {
                        $("[controltype=4][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                            $(this).find("input[type=text]").each(function (k) {
                                this.value = "";
                                var controlhide = "";
                                controlhide = $(this).parent().parent()[0].id;
                                if (controlhide !== "" && controlhide !== "undefined") {
                                    $('#' + controlhide).hide('slow');
                                }

                            });
                        });
                    }

                });
            });
        }
        else if (AnsType == 0) { // text

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                $(this).find("input[type=text]").each(function (k) {
                    this.value = "";
                });
            });
        }
        else if (AnsType == 1) { // textArea

            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                $(this).find("textarea").each(function (k) {
                    this.value = "";
                });
            });
        }
        else if (AnsType == 4) { // Dropdown
            $("[controltype=3][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                AnsId = this.getAttribute("AnswerId");
                var firstcontrolid = $(this).parent()[0].id;
                $('#' + firstcontrolid).prop('selectedIndex', 0).triggerHandler('change');
                var IsFq = "";
                IsFq = this.getAttribute("Isfq");

                if (IsFq == "1") {
                    $("[controltype=4][GroupId='" + grpId + "'][QuestionId='" + queID + "']").each(function (k) {
                        $(this).find("input[type=text]").each(function (k) {
                            this.value = "";
                        });
                    });
                }
            });
        }
    }
    return IsValidData;
}

function fillpfselection(rowno, grpID, queID, AnswerID, AnswerLable, fqans, otherid) {

    var pfsel = new Object();
    pfsel = {
        "rowid": dtrowno,
        "GroupId": grpID,
        "QuestionID": queID,
        "AnswerId": AnswerID,
        "AnswerLable": AnswerLable,
        "fqans": fqans
    };

    pfselection.push(pfsel);
}

var fqstartseperator = "@#fq1#%";
var fqendseperator = "@#fq2#%";
var ct_separator = "@@@@ct@@@@";
var ot_separator = "@@@@ot@@@@";
var QuestionID_separator = "@@@@QID@@@@";
var AnsType_separator = "@@@@Atype@@@@";
var grpid_separator = "@@@@grpid@@@@";
var rowid_separator = "@@@@rowid@@@@";

function FillData(data) {
    pfselection = [];
    try {

        //        var pfselection = data;

        for (var i = 0; i < data.length; i++) {

            var AnsType = getanswertype(data[i].nPFgrpid, data[i].nPFQuestionID);
            var othid = getotherid(data[i].nPFgrpid, data[i].nPFQuestionID, data[i].nPFAnswerId);
            var AnswerLable = "";
            dtrowno = data[i].nPFrowid;

            if (AnsType === "0") {
                fillpfselection(data[i].nPFrowid, data[i].nPFgrpid, data[i].nPFQuestionID, data[i].nPFAnswerId, data[i].sFollowedAnswerComment, "", othid)
            }
            else if (AnsType === "1") {
                fillpfselection(data[i].nPFrowid, data[i].nPFgrpid, data[i].nPFQuestionID, data[i].nPFAnswerId, data[i].sFollowedAnswerComment, "", othid)
            }
            else if (AnsType === "2") {
                $("[controltype=3][GroupId='" + data[i].nPFgrpid + "'][QuestionId='" + data[i].nPFQuestionID + "'][AnswerId='" + data[i].nPFAnswerId + "']").each(function (k) {
                    $(this).find("input[type=checkbox]").each(function (k) {
                        AnswerLable = this.getAttribute("AnswerLabel");
                    });
                });
                fillpfselection(data[i].nPFrowid, data[i].nPFgrpid, data[i].nPFQuestionID, data[i].nPFAnswerId, AnswerLable, data[i].sFollowedAnswerComment, othid)
            }
            else if (AnsType === "3") {
                $("[controltype=3][GroupId='" + data[i].nPFgrpid + "'][QuestionId='" + data[i].nPFQuestionID + "'][AnswerId='" + data[i].nPFAnswerId + "']").each(function (k) {
                    $(this).find("input[type=radio]").each(function (k) {
                        AnswerLable = this.getAttribute("AnswerLabel");
                    });
                });
                fillpfselection(data[i].nPFrowid, data[i].nPFgrpid, data[i].nPFQuestionID, data[i].nPFAnswerId, AnswerLable, data[i].sFollowedAnswerComment, othid)
            }
            else if (AnsType === "4") {
                $("[controltype=3][GroupId='" + data[i].nPFgrpid + "'][QuestionId='" + data[i].nPFQuestionID + "'][AnswerId='" + data[i].nPFAnswerId + "']").each(function (k) {
                    AnswerLable = this.getAttribute("AnswerLabel");
                });
                fillpfselection(data[i].nPFrowid, data[i].nPFgrpid, data[i].nPFQuestionID, data[i].nPFAnswerId, AnswerLable, data[i].sFollowedAnswerComment, othid)
            }
        }

        var grparray = [];
        var rowarray = [];

        grparray = getDistinctgroup();
        for (var i = 0; i < grparray.length; i++) {
            if (isdatatable(grparray[i]) === "1") continue;
            fillcontrols(grparray[i], 0);
        }

        for (var i = 0; i < grparray.length; i++) {
            if (isdatatable(grparray[i]) === "0") continue;
            rowarray = getDistinctrowid(grparray[i]);
            for (var j = 0; j < rowarray.length; j++) {
                dtrowno = rowarray[j]

                savedatatable(getdtdivid(grparray[i]), false);
            }
        }

        grparray = [];
        rowarray = [];

    } catch (e) {
    }
}

function fillcontrols(groupid, rowid) {
    for (var i = 0; i < pfselection.length; i++) {
        var nPFAnswerId = pfselection[i].AnswerId;
        var nPFQuestionID = pfselection[i].QuestionID;
        var sFollowedAnswerComment = pfselection[i].fqans;
        var grpid = pfselection[i].GroupId;
        var nPFAnswerType = getanswertype(grpid, nPFQuestionID);


        if (rowid === 0) {
            if (parseInt(pfselection[i].GroupId) !== parseInt(groupid)) {
                continue;
            }
        }
        else {
            if (!(parseInt(pfselection[i].GroupId) === parseInt(groupid) && parseInt(pfselection[i].rowid) === parseInt(rowid))) {
                continue;
            }
        }


        if (nPFAnswerType == "0") {
            $("[controltype=3][QuestionId='" + nPFQuestionID + "']").each(function (k) {
                $(this).find("input[type=text]").each(function (k1) {
                    $('#' + $(this)[0].id).val(pfselection[i].AnswerLable);
                });
            });
        }
        if (nPFAnswerType == "1") {
            $("[controltype=3][QuestionId='" + nPFQuestionID + "']").each(function (k) {
                $(this).find("textarea").each(function (k1) {
                    $('#' + $(this)[0].id).val(pfselection[i].AnswerLable);
                });
            });
        }
        if (nPFAnswerType == "2") {
            var IsFQ = "0";
            $("[controltype=3][AnswerId='" + nPFAnswerId + "']").each(function (k) {
                if (this.hasAttribute("Isfq")) {
                    IsFQ = this.getAttribute("Isfq");
                }
                $(this).find("input[type=checkbox]").each(function (k1) {
                    $('#' + $(this)[0].id).prop('checked', true).triggerHandler('click');
                });
            });
            if (IsFQ == "1") {
                $("[controltype=4][QuestionId='" + nPFQuestionID + "'][AnswerId='" + nPFAnswerId + "']").each(function (k) {
                    $(this).find("input[type=text]").each(function (k1) {
                        $('#' + $(this)[0].id).val(sFollowedAnswerComment);
                    });
                });
            }
        }
        if (nPFAnswerType == "3") {
            var IsFQ = "0";
            $("[controltype=3][AnswerId='" + nPFAnswerId + "']").each(function (k) {
                if (this.hasAttribute("Isfq")) {
                    IsFQ = this.getAttribute("Isfq");
                }

                $(this).find("input[type=radio]").each(function (k1) {
                    $('#' + $(this)[0].id).prop('checked', true).triggerHandler('click');
                });
            });
            if (IsFQ == "1") {
                $("[controltype=4][QuestionId='" + nPFQuestionID + "']").each(function (k) {
                    $(this).find("input[type=text]").each(function (k1) {
                        $('#' + $(this)[0].id).val(sFollowedAnswerComment);
                    });
                });
            }
        }
        if (nPFAnswerType == "4") {
            var IsFQ = "0";
            $("[controltype=3][QuestionId='" + nPFQuestionID + "'][AnswerId='" + nPFAnswerId + "']").each(function (k) {
                if (this.hasAttribute("Isfq")) {
                    IsFQ = this.getAttribute("Isfq");
                }
                var vid = '#' + $(this).parent()[0].id;
                var strval = this.value; //this.getAttribute("AnswerLabel");
                $(vid).val(strval).attr('selected', 'selected').triggerHandler('change');
            });
            if (IsFQ == "1") {
                $("[controltype=4][QuestionId='" + nPFQuestionID + "']").each(function (k) {
                    $(this).find("input[type=text]").each(function (k1) {
                        $('#' + $(this)[0].id).val(sFollowedAnswerComment);
                    });
                });
            }
        }
    }
}


function getDistinctgroup() {
    var result = [];
    for (var i = 0; i < pfselection.length; i++) {
        if (result.indexOf(pfselection[i].GroupId) === -1) {
            result.push(pfselection[i].GroupId);
        }
    }
    return result;
}

function getDistinctrowid(grpid) {

    var result = [];
    for (var i = 0; i < pfselection.length; i++) {
        if (grpid === pfselection[i].GroupId) {
            if (result.indexOf(pfselection[i].rowid) === -1) {
                result.push(pfselection[i].rowid);
            }
        }
    }
    return result;
}


function getDistinctrowidbygrpandquestion(grpid, quesid) {

    var result = [];
    for (var i = 0; i < pfselection.length; i++) {
        if (grpid === pfselection[i].GroupId && pfselection[i].QuestionID === quesid) {
            if (result.indexOf(pfselection[i].rowid) === -1) {
                result.push(pfselection[i].rowid);
            }
        }
    }
    return result;
}

function getdtdivid(grpid) {
    var dtdivid = "";
    try {
        $("[controltype=1][GroupId = " + grpid + "]").each(function (k) {
            var grpdivid = $(this)[0].getAttribute("id");
            $("[datatable=1][parentid = " + grpdivid + "]").each(function (k) {
                dtdivid = $(this)[0].getAttribute("id");
            });
        });
        return dtdivid;
    } catch (e) {
        return "";
    }
}

var Resultdata = [];

function Submitdata() {

    $('#modalHistoryConfirmation').modal('hide');
    Resultdata = [];

    for (var i = 0; i < question.length; i++) {
        var isanspresent = false;
        for (var j = 0; j < pfselection.length; j++) {
            if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID) {
                isanspresent = true;
                break;
            }
        }

        if (isanspresent === false)
            continue;
        var AnswerLables = question[i].AnswerLables;
        var HistoryCategoryId = question[i].HistoryCategoryId;
        var HistoryItemId = question[i].HistoryItemId;
        var AnswerIDs = "";
        var OtherIDs = "";
        var rowids = "";

        var HistoryCategory = question[i].HistoryCategory;
        var HistoryItem = question[i].historyitem;
        var HistoryCategoryType = question[i].HistoryCategoryType;

        var questiontype = question[i].QuestionType;
        var isdatatable = question[i].isdatatable;

        if (isdatatable === "0") {
            if (questiontype == "1") {

                HistoryItemId = question[i].HistoryItemId;
                HistoryItem = question[i].historyitem;

                for (var j = 0; j < pfselection.length; j++) {

                    if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID) {
                        var AnsId = pfselection[j].AnswerId;
                        if (AnsId === null || AnsId === undefined) {
                            AnsId = "0";
                        }
                        var OthID = getotherid(question[i].GroupId, question[i].QuestionID, AnsId);
                        if (OthID === null || OthID === undefined) {
                            OthID = "0";
                        }

                        if (question[i].AnswerType === "0" || question[i].AnswerType === "1") {
                            AnswerLables += fqstartseperator + pfselection[j].AnswerLable + fqendseperator + ", ";
                        }
                        else {
                            if (pfselection[j].fqans.trim() === "") {
                                AnswerLables += pfselection[j].AnswerLable + ", ";
                            }
                            else {
                                AnswerLables += pfselection[j].AnswerLable + " " + fqstartseperator + pfselection[j].fqans + fqendseperator + ", ";
                            }
                        }
                        AnswerIDs += AnsId + ",";
                        OtherIDs += OthID + ",";
                        rowids += pfselection[j].rowid + ",";
                    }
                }
                if (AnswerLables == "")
                    continue;
                Resultdata.push(HistoryCategoryId + "~" + HistoryItemId + "~" + AnswerIDs + ' $ ' + AnswerLables + ct_separator + HistoryCategoryType + ot_separator + OtherIDs + QuestionID_separator + question[i].QuestionID + AnsType_separator + question[i].AnswerType + grpid_separator + question[i].GroupId + rowid_separator + rowids);
            }
            else if (questiontype == "2") {

                for (var j = 0; j < pfselection.length; j++) {
                    if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID) {
                        var AnsId = pfselection[j].AnswerId;
                        if (AnsId === null || AnsId === undefined) {
                            AnsId = "0";
                        }
                        var OthID = getotherid(question[i].GroupId, question[i].QuestionID, AnsId);
                        if (OthID === null || OthID === undefined) {
                            OthID = "0";
                        }
                        HistoryItemId = gethistoryitemid(question[i].GroupId, question[i].QuestionID, AnsId);
                        if (HistoryItemId === null || HistoryItemId === undefined) {
                            HistoryItemId = "0";
                        }

                        if (question[i].AnswerType === "0" || question[i].AnswerType === "1") {
                            AnswerLables = fqstartseperator + pfselection[j].AnswerLable + fqendseperator;
                        }
                        else {
                            if (pfselection[j].fqans.trim() === "") {
                                AnswerLables = pfselection[j].AnswerLable;
                            }
                            else {
                                AnswerLables = pfselection[j].AnswerLable + " " + fqstartseperator + pfselection[j].fqans + fqendseperator;
                            }
                        }
                        Resultdata.push(HistoryCategoryId + "~" + HistoryItemId + "~" + AnsId + ' $ ' + AnswerLables + ct_separator + HistoryCategoryType + ot_separator + OthID + QuestionID_separator + question[i].QuestionID + AnsType_separator + question[i].AnswerType + grpid_separator + question[i].GroupId + rowid_separator + pfselection[j].rowid);
                    }
                }
            }
        }
        else if (isdatatable === "1") {
            if (questiontype == "1") {

                var temprowno = 0;
                var rowarray = [];
                rowarray = getDistinctrowidbygrpandquestion(question[i].GroupId, question[i].QuestionID);
                for (var k = 0; k < rowarray.length; k++) {
                    temprowno = rowarray[k];

                    HistoryItemId = question[i].HistoryItemId;
                    HistoryItem = question[i].historyitem;

                    AnswerLables = "";
                    AnswerIDs = "";
                    OtherIDs = "";
                    rowids = "";

                    for (var j = 0; j < pfselection.length; j++) {

                        if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID && pfselection[j].rowid === temprowno) {
                            var AnsId = pfselection[j].AnswerId;
                            if (AnsId === null || AnsId === undefined) {
                                AnsId = "0";
                            }
                            var OthID = getotherid(question[i].GroupId, question[i].QuestionID, AnsId);
                            if (OthID === null || OthID === undefined) {
                                OthID = "0";
                            }

                            if (question[i].AnswerType === "0" || question[i].AnswerType === "1") {
                                AnswerLables += fqstartseperator + pfselection[j].AnswerLable + fqendseperator + ", ";
                            }
                            else {
                                if (pfselection[j].fqans.trim() === "") {
                                    AnswerLables += pfselection[j].AnswerLable + ", ";
                                }
                                else {
                                    AnswerLables += pfselection[j].AnswerLable + " " + fqstartseperator + pfselection[j].fqans + fqendseperator + ", ";
                                }
                            }
                            AnswerIDs += AnsId + ",";
                            OtherIDs += OthID + ",";
                            rowids += pfselection[j].rowid + ",";
                        }
                    }
                    if (AnswerLables == "")
                        continue;
                    Resultdata.push(HistoryCategoryId + "~" + HistoryItemId + "~" + AnswerIDs + ' $ ' + AnswerLables + ct_separator + HistoryCategoryType + ot_separator + OtherIDs + QuestionID_separator + question[i].QuestionID + AnsType_separator + question[i].AnswerType + grpid_separator + question[i].GroupId + rowid_separator + rowids);
                }


            }
            else if (questiontype == "2") {
                var temprowno = 0;
                var rowarray = [];
                rowarray = getDistinctrowidbygrpandquestion(question[i].GroupId, question[i].QuestionID);
                for (var k = 0; k < rowarray.length; k++) {
                    temprowno = rowarray[k];

                    AnswerLables = "";
                    AnswerIDs = "";
                    OtherIDs = "";
                    rowids = "";

                    for (var j = 0; j < pfselection.length; j++) {
                        if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID && pfselection[j].rowid === temprowno) {
                            var AnsId = pfselection[j].AnswerId;
                            if (AnsId === null || AnsId === undefined) {
                                AnsId = "0";
                            }
                            var OthID = getotherid(question[i].GroupId, question[i].QuestionID, AnsId);
                            if (OthID === null || OthID === undefined) {
                                OthID = "0";
                            }
                            HistoryItemId = gethistoryitemid(question[i].GroupId, question[i].QuestionID, AnsId);
                            if (HistoryItemId === null || HistoryItemId === undefined) {
                                HistoryItemId = "0";
                            }

                            if (question[i].AnswerType === "0" || question[i].AnswerType === "1") {
                                AnswerLables = fqstartseperator + pfselection[j].AnswerLable + fqendseperator;
                            }
                            else {
                                if (pfselection[j].fqans.trim() === "") {
                                    AnswerLables = pfselection[j].AnswerLable;
                                }
                                else {
                                    AnswerLables = pfselection[j].AnswerLable + " " + fqstartseperator + pfselection[j].fqans + fqendseperator;
                                }
                            }
                            Resultdata.push(HistoryCategoryId + "~" + HistoryItemId + "~" + AnsId + ' $ ' + AnswerLables + ct_separator + HistoryCategoryType + ot_separator + OthID + QuestionID_separator + question[i].QuestionID + AnsType_separator + question[i].AnswerType + grpid_separator + question[i].GroupId + rowid_separator + pfselection[j].rowid);
                        }
                    }
                }
            }
        }


        //Resultdata.push(HistoryCategoryId + "~" + HistoryItemId + "~" + AnswerIDs + "~" + AnswerLables);
        // Result.push(HistoryCategory + "~" + HistoryItem + "~" + AnswerLables);
    }
    if ((Resultdata.length > 0)) {
        $("#hdnResult").val(Resultdata.join("#$rp!bv*kf%us$#"));
        SubmitHistory()
    }
    else {
        $('#SpanValidaitonMessage').html("Please enter information to submit.");
        $('#modalValidationMsg').modal();
    }
}

function getotherid(grpid, queid, ansid) {
    try {
        var OtherID = 0;
        for (var i = 0; i < Answer.length; i++) {
            if (Answer[i].GroupId === grpid && Answer[i].QuestionID === queid && Answer[i].AnswerId === ansid) {
                OtherID = Answer[i].OtherID;
                break;
            }
        }
        return OtherID;
    } catch (e) {
        return 0;
    }
}

function gethistoryitemid(grpid, queid, ansid) {
    try {
        var historyitemid = 0;
        for (var i = 0; i < Answer.length; i++) {
            if (Answer[i].GroupId === grpid && Answer[i].QuestionID === queid && Answer[i].AnswerId === ansid) {
                historyitemid = Answer[i].historyitemid;
                break;
            }
        }
        return historyitemid;
    } catch (e) {
        return 0;
    }
}


function getanswertype(grpid, queid) {
    try {
        var AnsType = "";
        for (var i = 0; i < question.length; i++) {
            if (question[i].GroupId === grpid && question[i].QuestionID === queid) {
                AnsType = question[i].AnswerType;
                break;
            }
        }
        return AnsType;
    } catch (e) {
        return "";
    }
}


function getgrouplabel(grpid) {
    try {
        var grplabel = "";
        for (var i = 0; i < group.length; i++) {
            if (group[i].GroupId === grpid) {
                grplabel = group[i].grouplabel;
                break;
            }
        }
        return grplabel;
    } catch (e) {
        return "";
    }
}


function isdatatable(grpid) {
    try {
        var isdt = "0";
        for (var i = 0; i < group.length; i++) {
            if (group[i].GroupId === grpid) {
                isdt = group[i].isdatatable;
                break;
            }
        }
        return isdt;
    } catch (e) {
        return "0";
    }
}

function getfq() {
    $("[controltype=4]").each(function (k) {
        var fq = new Object();

        var cntrlType = this.getAttribute("controltype");
        var category = this.getAttribute("HistoryCategory");
        var grpID = this.getAttribute("GroupId");
        var historyItem = this.getAttribute("historyitem");
        var queID = this.getAttribute("QuestionID");
        var answerID = this.getAttribute("answerID");
        fq = { "controltype": cntrlType, "HistoryCategory": category, "GroupId": grpID, "historyitem": historyItem, "QuestionID": queID, "answerID": answerID };
        followedQA.push(fq);
    });
}
// ----------------- End New Logic ---------------------

var Medcount = 0;
var Result = [];
var RowCount = 0;

function SubmitForm() {

    Type = "POST";
    Url = globalServiceURL + "PutHistory";
    //    Url = "http://localhost:35798/RestServiceImpl.svc/PutHistory";

    //Patient Form Content


    var Category;
    var Item;
    var Comment;
    var thisid;
    var Result = [];
    var txtClass;
    var IsChild;

    var IsValid = true;

    //Function to collect data from History Form
    //To Identity Category, Item we have bind custom arributes "category" and "HItem"
    // to followed question we have find custom flag i.e "IsChild" and this attribute only have for text and textarea
    $(".HistoryData input").each(function (i) {

        if (this.attributes["category"] != undefined) {


            Category = this.attributes["category"].nodeValue;
            Item = this.attributes["HItem"].nodeValue;

            if (this.type == 'checkbox') { // if control in checkbox
                if (document.getElementById(this.id).checked == true) {
                    // Collect data if check box has follwing question. Text box has add class with ID for checked checkbox
                    thisid = this.id;
                    Comment = $("." + thisid).val();

                    if (Comment == undefined || Comment == "") {
                        // if checkbox did not contain following question then collect the comment from self tag.
                        if (this.attributes["Hcomm"] != undefined)
                            Comment = this.attributes["Hcomm"].nodeValue;
                    }
                    if (Comment == undefined || Comment == "") {
                        // if the self tag did not contains comments then add checked as comment in array.
                        //Bug #67179: Past Medical History comment shows true value
                        //Comment old code added new code to show blank.
                        //Comment = document.getElementById(this.id).checked;
                        Comment = this.attributes["Hcomm"].nodeValue;
                        //Result.push('Category: ' + Category + ',' + 'Item: ' + Item + ',' + 'Comment: ' + Comment);
                        if (Comment.length > 500) {
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValid = false;
                            return false;
                        }
                        Result.push(Category + '~' + Item + '~' + Comment);
                    }
                    else {

                        //Result.push('Category: ' + Category + ',' + 'Item: ' + Item + ',' + 'Comment: ' + Comment);
                        if (Comment.length > 500) {
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValid = false;
                            return false;
                        }
                        Result.push(Category + '~' + Item + '~' + Comment);
                    }
                }

            }
            else if (this.type == "radio") { // if control is radio
                if (document.getElementById(this.id).checked == true) {
                    thisid = this.id;
                    Comment = "";
                    // Collect data if radio button has follwing question. Text box has add class with ID of checked radio button

                    $("." + thisid).each(function () { // if following question contains more than one comment(text box)
                        if (Comment == undefined || Comment == "") {
                            if (this.attributes["hcomm"] == undefined || this.attributes["hcomm"] == "") {
                                Comment = $(this).val();
                            }
                            else {
                                Comment = this.attributes["hcomm"].nodeValue + ", Quantity: " + $(this).val();
                            }
                        }
                        else {
                            if (this.attributes["hcomm"] == undefined || this.attributes["hcomm"] == "") {
                                Comment = Comment + ',' + $(this).val();
                            }
                            else {
                                Comment = Comment + ',' + "How long: " + $(this).val();
                            }
                        }
                    });
                    //  Comment = $("." + thisid).val();
                    if (Comment == undefined || Comment == "") {
                        // Comment = document.getElementById(this.id).checked;
                        // if control did not contain following question then try to collect data from own tag
                        Comment = this.attributes["Hcomm"].nodeValue;
                        //Result.push('Category: ' + Category + ',' + 'Item: ' + Item + ',' + 'Comment: ' + Comment);
                        if (Comment.length > 500) {
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValid = false;
                            return false;
                        }
                        Result.push(Category + '~' + Item + '~' + Comment);
                        Comment = "";
                    }
                    else {
                        //Result.push('Category: ' + Category + ',' + 'Item: ' + Item + ',' + 'Comment: ' + Comment);
                        if (Comment.length > 500) {
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValid = false;
                            return false;
                        }
                        Result.push(Category + '~' + Item + '~' + Comment);
                        Comment = "";
                    }
                }
            }
            else if (this.type == "text") { // if control is textbox
                IsChild = this.attributes["IsChild"].nodeValue;

                if (IsChild == "false") { // check is the question is not following question because it's data is already collected from in radio or checkbox control
                    if (document.getElementById(this.id).value != "") {
                        Comment = document.getElementById(this.id).value;
                        if (Comment.length > 500) {
                            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
                            $('#modalValidationMsg').modal();
                            IsValid = false;
                            return false;
                        }
                        //Result.push('Category: ' + Category + ',' + 'Item: ' + Item + ',' + 'Comment: ' + Comment);
                        Result.push(Category + '~' + Item + '~' + Comment);
                        Comment = "";
                    }
                }

            }
        }
    });

    $(".HistoryData textarea").each(function (i) { // function to collect data from text area control
        IsChild = this.attributes["IsChild"].nodeValue;
        if (IsChild == "false") {
            if (document.getElementById(this.id).value != "") {
                Category = this.attributes["category"].nodeValue;
                Item = this.attributes["HItem"].nodeValue;
                Comment = document.getElementById(this.id).value;
                if (Comment.length > 500) {
                    $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
                    $('#modalValidationMsg').modal();
                    IsValid = false;
                    return false;
                }
                // Result.push('Category: ' + Category + ',' + 'Item: ' + Item + ',' + 'Comment: ' + Comment);
                Result.push(Category + '~' + Item + '~' + Comment);
            }
        }
    });

    $("#dynamicTable table").each(function (i) {
        Category = this.attributes["category"].nodeValue;
        Category = Category.replace(/\~/g, ' ');
        Item = this.attributes["HItem"].nodeValue;
        Item = Item.replace(/\~/g, ' ');
        Comment = this.attributes["PatientMed"].nodeValue;
        Comment = Comment.replace(/\~/g, ' ');
        if (Comment.length > 500) {
            $('#SpanValidaitonMessage').html("Comment should be maximum 500 characters for <b>" + Item + "</b> in <b>" + Category + "</b> section");
            $('#modalValidationMsg').modal();
            IsValid = false;
            return false;
        }
        Result.push(Category + '~' + Item + '~' + Comment);
    });

    $("#hdnResult").val(Result.join("#$rp!bv*kf%us$#"));
    if (IsValid == true) {
        if ((Result.length > 0)) {
            CheckSubmit();
            //        var msg2 = { "SerachRequest": { "SessionId": sessionStorage.PatientPortalSessionID, "PracticeId": globalPracticeID },
            //            "crt": { "data": Result, "patientid": sessionStorage.LoggedInUserID }

            //        };


            //        Data = JSON.stringify(msg2);
            //        ContentType = "application/json; charset=utf-8";
            //        DataType = "json"; ProcessData = true;
            //        method = "SubmitForm";
            //        CallService();
        }
        else {
            ValidateForm();
        }
    }
}


//function CallService() {


//    $.ajax({

//        type: Type, //GET or POST or PUT or DELETE verb
//        url: Url, // Location of the service
//        data: Data, //Data sent to server
//        contentType: ContentType, // content type sent to server
//        dataType: DataType, //Expected data format from server                
//        processdata: ProcessData, //True or False

//        success: function (oRequest) {//On Successfull service call                  

//            ServiceSucceeded(oRequest);
//        },
//        error: ServiceFailed// When Service call fails
//    });

//}



//function ServiceFailed(xhr) {
//    alert(xhr.responseText);
//    if (xhr.responseText) {
//        var err = xhr.responseText;
//        if (err)
//            error(err);
//        else
//            error({ Message: "Unknown server error." })
//    }
//    return;
//}

function Showtextchk(Divid) { // show Hide following question's text box for check boxes

    if (document.getElementById(Divid).style.display == "none")
        document.getElementById(Divid).style.display = "block";
    else {

        document.getElementById(Divid).style.display = "none";
    }
}

//function showchkfq(cntrlID,tr1_ID,tr2_ID,fqID,fqLabel) {

//    if (fqID != '') {
//        document.getElementById(fqID).innerHTML = fqLabel;

//        if (document.getElementById(tr1_ID).style.display == "none") {
//            document.getElementById(tr1_ID).style.display = "Block";
//            document.getElementById(tr2_ID).style.display = "Block";
//        }
//        else {
//            document.getElementById(fqID).innerHTML = "";
//            document.getElementById(tr1_ID).style.display = "none";
//            document.getElementById(tr2_ID).style.display = "none";
//        }
//    }
//    else {
//        document.getElementById(tr1_ID).style.display = "none";
//        document.getElementById(tr2_ID).style.display = "none";
//    }

//}


function showchkfq(cntrl1ID, div_ID) {

    if (document.getElementById(cntrl1ID).checked == true) {
        if (div_ID != '') {
            document.getElementById(div_ID).style.display = "block";
        }
    }
    else if (document.getElementById(cntrl1ID).checked == false) {
        if (div_ID != '') {
            document.getElementById(div_ID).style.display = "none";
        }
    }

}

function showrbtfq(cntrlID, div_ID, fqID, fqLabel) {
    if (document.getElementById(cntrlID).checked == true) {
        if (fqID != '') {
            document.getElementById(fqID).innerHTML = fqLabel;
            document.getElementById(div_ID).style.display = "block";
        }
        else {
            document.getElementById(div_ID).style.display = "none";
        }

    }

}

function showDropDown(cntrlID) {


    var splitted = document.getElementById(cntrlID).value.split("|||");

    if (splitted[0] == "1") {
        document.getElementById('div_' + splitted[1]).style.display = "block";
        document.getElementById('fq_' + splitted[2]).innerHTML = splitted[3];
    }
    else {
        document.getElementById('fq_' + splitted[2]).innerHTML = "";
        document.getElementById('div_' + splitted[1]).style.display = "none";

    }
}

var deletegrpid = 0;
var deleterowid = 0;

var editgrpid = 0;
var editrowid = 0;


function deletegroup() {
    deletepfselection(deletegrpid, deleterowid);
    deletehtmlbydiv(deletegrpid, deleterowid);
    deletegrpid = 0;
    deleterowid = 0;
}

function deletehtmlbydiv(grpid, rowid) {
    $("[GroupId='" + grpid + "'][RowId='" + rowid + "']").remove();
}

function edithtmlbydiv(grpid, rowid, divhtml) {
    //$("[GroupId='" + grpid + "'][RowId='" + rowid + "']").replaceWith(divhtml);
    $("[GroupId='" + grpid + "'][RowId='" + rowid + "'][type='2']").html(divhtml);
}


function deletepfselection(grpid, rowid) {

    if (pfselection.length > 0) {
        for (var i = pfselection.length - 1; i >= 0; i--) {
            if (parseInt(pfselection[i].GroupId) === parseInt(grpid) && parseInt(pfselection[i].rowid) === parseInt(rowid)) {
                pfselection.splice(i, 1);
            }
        }
    }
}

function deletegroupbyrowid(grpid, rowid) {
    try {
    deletegrpid = grpid;
    deleterowid = rowid;
    $('#divdeleteconfirmation').modal();
} catch (e) {
}
}


function editgroupbyid(grpid, rowid) {
    try {
        isEdit = 1;
        editgrpid = grpid;
        editrowid = rowid;
        deletegrpid = grpid;
        deleterowid = rowid;
        fillcontrols(grpid, rowid);
    } catch (e) {
        isEdit = 0;
    }
}

var dtrowno = 0;
var isEdit = 0;

function savedatatable(dtID, collectdata) {
    try {
        var grpdivid = $('#' + dtID)[0].getAttribute("parentid");
        var grpid = $('#' + grpdivid)[0].getAttribute("GroupId");

        var collectdata2 = collectdata;
        if (isEdit === 1) {
            deletepfselection(editgrpid, editrowid);
            collectdata = false;
            dtrowno = editrowid;
            if (getPfselection(true, grpid) === false) {
                return;
            }
        }
        if (collectdata === true) {
            var temprowno = 0;
            for (var i = 0; i < pfselection.length; i++) {
                if (pfselection[i].GroupId === grpid) {
                    if (temprowno < pfselection[i].rowid) {
                        temprowno = pfselection[i].rowid;
                    }
                }
            }
            if (temprowno === 0) {
                dtrowno = 1;
            }
            else {
                dtrowno = parseInt(temprowno) + 1;
            }
            if (getPfselection(true, grpid) === false) {
                return;
            }
        }

        var divstr = "";
        if (isEdit === 0) {
            divstr += " <div class='row-fluid' type='1' GroupId='" + grpid + "' RowId = '" + dtrowno + "'><span class='span12'></span></div>";
            divstr += " <div class='container-fluid' type='2' GroupId='" + grpid + "' RowId = '" + dtrowno + "'>";
        }
        divstr += " <hr class='main' />";
        divstr += " <div class='row-fluid'>";
        divstr += " <div class='span4'><strong></strong></div>";
        divstr += " <div class='span8'>";
        divstr += " <strong><a id='A1' onclick=\"editgroupbyid('" + grpid + "','" + dtrowno + "');\">Edit</a></strong> ";
        divstr += " &nbsp;&nbsp;&nbsp;&nbsp;";
        divstr += " <strong><a id='A2' onclick=\"deletegroupbyrowid('" + grpid + "','" + dtrowno + "');\">Delete</a></strong>";
        divstr += " </div> ";
        divstr += " </div> ";
        divstr += " <hr class='main' />";

        var exists = false;
        var AnswerIDs = "";
        var OtherIDs = "";
        var AnswerLables = "";

        for (var i = 0; i < question.length; i++) {

            if (question[i].GroupId !== grpid) {
                continue;
            }
            var questiontype = question[i].QuestionType;
            if (questiontype == "1") {

                HistoryItemId = question[i].HistoryItemId;
                HistoryItem = question[i].historyitem;

                AnswerLables = "";
                AnswerIDs = "";
                OtherIDs = "";
                rowids = "";

                for (var j = 0; j < pfselection.length; j++) {

                    if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID && pfselection[j].rowid === dtrowno) {
                        var AnsId = pfselection[j].AnswerId;
                        if (AnsId === null || AnsId === undefined) {
                            AnsId = "0";
                        }
                        var OthID = getotherid(question[i].GroupId, question[i].QuestionID, AnsId);
                        if (OthID === null || OthID === undefined) {
                            OthID = "0";
                        }

                        if (question[i].AnswerType === "0" || question[i].AnswerType === "1") {
                            AnswerLables += pfselection[j].AnswerLable + ", ";
                        }
                        else {
                            if (pfselection[j].fqans.trim() === "") {
                                AnswerLables += pfselection[j].AnswerLable + ", ";
                            }
                            else {
                                AnswerLables += pfselection[j].AnswerLable + "(" + pfselection[j].fqans + ")" + ", ";
                            }
                        }
                        AnswerIDs += AnsId + ",";
                        OtherIDs += OthID + ",";
                        rowids += pfselection[j].rowid + ",";
                    }
                }
                if (AnswerLables == "")
                    continue;

                AnswerLables = AnswerLables.trim();
                if (AnswerLables.substr(AnswerLables.length - 1, 1) === ",") {
                    AnswerLables = AnswerLables.slice(0, -1);
                }
                exists = true;
                divstr += " <div class='row-fluid'>";
                divstr += " <div class='span4'><strong>" + question[i].questionlabel + ":</strong></div>";
                divstr += " <div class='span8'>" + AnswerLables + "</div> ";
                divstr += " </div>";

            }
            else if (questiontype == "2") {


                AnswerLables = "";
                AnswerIDs = "";
                OtherIDs = "";
                rowids = "";

                for (var j = 0; j < pfselection.length; j++) {
                    if (question[i].GroupId === pfselection[j].GroupId && question[i].QuestionID === pfselection[j].QuestionID && pfselection[j].rowid === dtrowno) {
                        var AnsId = pfselection[j].AnswerId;
                        if (AnsId === null || AnsId === undefined) {
                            AnsId = "0";
                        }
                        var OthID = getotherid(question[i].GroupId, question[i].QuestionID, AnsId);
                        if (OthID === null || OthID === undefined) {
                            OthID = "0";
                        }
                        HistoryItemId = gethistoryitemid(question[i].GroupId, question[i].QuestionID, AnsId);
                        if (HistoryItemId === null || HistoryItemId === undefined) {
                            HistoryItemId = "0";
                        }
                        if (question[i].AnswerType === "0" || question[i].AnswerType === "1") {
                            AnswerLables = pfselection[j].AnswerLable;
                        }
                        else {
                            if (pfselection[j].fqans.trim() === "") {
                                AnswerLables = pfselection[j].AnswerLable;
                            }
                            else {
                                AnswerLables = pfselection[j].AnswerLable + "(" + pfselection[j].fqans + ")";
                            }
                        }
                        AnswerLables = AnswerLables.trim();
                        if (AnswerLables.substr(AnswerLables.length - 1, 1) === ",") {
                            AnswerLables = AnswerLables.slice(0, -1);
                        }
                        exists = true;
                        divstr += " <div class='row-fluid'>";
                        divstr += " <div class='span4'><strong>" + question[i].questionlabel + ":</strong></div>";
                        divstr += " <div class='span8'>" + AnswerLables + "</div> ";
                        divstr += " </div>";
                        //Resultdata.push(HistoryCategoryId + "~" + HistoryItemId + "~" + AnsId + ' $ ' + AnswerLables + ct_separator + HistoryCategoryType + ot_separator + OthID + QuestionID_separator + question[i].QuestionID + AnsType_separator + question[i].AnswerType + grpid_separator + question[i].GroupId + rowid_separator + pfselection[j].rowid);
                    }

                }
            }

        }

        divstr += " <hr class='main' />";
        if (isEdit === 0) {
            divstr += " </div> ";
        }
        if (isEdit === 0) {
            if (exists === true)
                $("#" + dtID).append(divstr);
        }
        else {
            if (exists === true) {
                edithtmlbydiv(editgrpid, editrowid, divstr);
            }
        }



        if (collectdata2 === true) {
            resetcontrols(dtID, collectdata);
            //return;
        }

        isEdit = 0;

    } catch (e) {
        
    }
}

function Edit(divId, rowID, maindivID) {
    alert(divId.id)
}

function Delete(divId, rowID, maindivID) {


    $("#" + divId.id).remove();

    //    alert(divId)
    //    var x = $("#" + divId)
    //    $("#" + maindivID).remove(x);


}

function ShowMedSection(Divid, Medstatus) { // show Hide following question's text box for check boxes

    if (document.getElementById(Divid).style.display == "none") {
        document.getElementById(Divid).style.display = "Block";
        if (Medstatus == "Add")
            $("#MedicationSec").text('Click here to cancel adding');
        else {
            $("#MedicationSec").text('Click here to cancel editing');
        }
    }
    else {

        document.getElementById(Divid).style.display = "none";
        $("#MedicationSec").text('Medication - Click here to add an entry');
        ResetMedControl();
    }
    ModifiedTable = undefined;
}

function Showtextrd(Divid, status) { // show Hide following question's text box for radio buttons

    if (status == 'block')
        document.getElementById(Divid).style.display = "block";
    else {

        document.getElementById(Divid).style.display = "none";
    }


}

function Showmulittextrd(Controlstatus, DivID1, DivID2, DivID3) { // show Hide following question's text box which has mulitiple textbox.

    if (Controlstatus == 'Yes') {
        document.getElementById(DivID1).style.display = "block";
        document.getElementById(DivID2).style.display = "none";
        document.getElementById(DivID3).style.display = "none";
    }
    else if (Controlstatus == 'No') {
        document.getElementById(DivID1).style.display = "none";
        document.getElementById(DivID2).style.display = "none";
        document.getElementById(DivID3).style.display = "none";
    }
    else if (Controlstatus == 'Occasionally') {
        document.getElementById(DivID1).style.display = "block";
        document.getElementById(DivID2).style.display = "none";
        document.getElementById(DivID3).style.display = "none";
    }
    else if (Controlstatus == 'Past') {
        document.getElementById(DivID1).style.display = "block";
        document.getElementById(DivID2).style.display = "none";
        document.getElementById(DivID3).style.display = "none";
    }


}

function AddMedication() {

    var MedName = $(".Med_Name").val();
    var MedDosages = $(".Med_Dosage").val();
    var MedDosUnit = $('#Med_DosageUnit>option:selected').text();
    var Freq = $('#Med_Frequency>option:selected').text();
    var MedStatus = $('#Med_Status>option:selected').text();

    if (MedName == undefined || MedName == "") {

        $('#ErrMsg').html("Field <b>'Medication Name'</b> is required.");
        $('#modalHealthForm').modal();
        return false;
    }

    if (MedStatus == undefined || MedStatus == "" || MedStatus == "Choose One") {
        //  alert("Field 'Status:' is required");
        $('#ErrMsg').html("Field <b>'Status'</b> is required.");
        $('#modalHealthForm').modal();
        return false;
    }

    var PatientMedication;
    MedName = MedName.replace(/\ /g, '~');
    MedDosages = MedDosages.replace(/\ /g, '~');
    MedDosUnit = MedDosUnit.replace(/\ /g, '~');
    Freq = Freq.replace(/\ /g, '~');
    MedStatus = MedStatus.replace(/\ /g, '~');

    if (Freq == "Choose~One") {
        Freq = undefined;
    }

    if (MedDosUnit == "Choose~One") {
        MedDosUnit = undefined;
    }

    if (MedDosages == "") {
        MedDosages = undefined;
    }
    if (ModifiedTable == undefined || ModifiedTable == "") {
        //  $("#dynamicTable table").length;
        Medcount = $("#dynamicTable table").length + 1;

        var tableID = "table" + Medcount;
        PatientMedication = "Medication~Name:" + MedName + ";Status:" + MedStatus + "";
        //
        if (MedDosages != "" && MedDosages != undefined) {
            PatientMedication = PatientMedication + "Dosage:" + MedDosages + "";
        }

        if (MedDosUnit != "" && MedDosUnit != undefined) {
            PatientMedication = PatientMedication + "Dosage~Unit:" + MedDosUnit + "";
        }

        if (Freq != "" && Freq != undefined) {
            PatientMedication = PatientMedication + "Frequency:" + Freq + "";
        }
        // create table
        var $table = $('<table id = ' + tableID + ' PatientMed = ' + PatientMedication + ' category = Patient~Reported~Medications HItem = Patient~Reported~Medication style="margin-left: 80px; margin-top: 15px; margin-bottom: 10px;">');

        // thead
        //.append('<thead>').children('thead')
        //.append('<tr />').children('tr').append('<th>A</th><th>B</th><th>C</th><th>D</th>');

        //tbody
        var $tbody = $table.append('<tbody />').children('tbody');

        // add row for Medication No.
        $tbody.append('<tr />').children('tr:last')
       .append("<td colspan=2><strong>Medication #" + Medcount + "</strong> - <strong><a href='#' id=Edit" + Medcount + " onclick='EditMedication(" + tableID + ");return false;'>Edit</a></strong>- <strong><a href='#' id=Delete" + Medcount + " onclick='DeleteMedication(" + tableID + ");return false;'>Delete</a></strong></td>")


        // add row for Medication Name + Dosage + Dosage unit




        if ($('#Med_DosageUnit>option:selected').text() != "Choose One") {
            $tbody.append('<tr />').children('tr:last')
       .append("<td colspan=2 Mitem = " + MedName + " Mdos = " + MedDosages + " MdosUnit = " + MedDosUnit + " ><span>" + $(".Med_Name").val() + "</span> <span>" + $(".Med_Dosage").val() + "</span> <span>" + $('#Med_DosageUnit>option:selected').text() + "</span></td>")
        }
        else {
            $tbody.append('<tr />').children('tr:last')
       .append("<td colspan=2 Mitem = " + MedName + " Mdos = " + MedDosages + " MdosUnit = " + MedDosUnit + " ><span>" + $(".Med_Name").val() + "</span> <span>" + $(".Med_Dosage").val() + "</span></td>")
        }

        // add Row for Frequency

        if ($('#Med_Frequency>option:selected').text() != "Choose One") {
            $tbody.append('<tr />').children('tr:last')
       .append("<td colspan=2 Mitem = " + Freq + "> Frequency - " + $('#Med_Frequency>option:selected').text() + "</td>")
        }

        // add Row for Status

        $tbody.append('<tr />').children('tr:last')
        .append("<td colspan=2 Mitem = " + MedStatus + ">Status - " + $('#Med_Status>option:selected').text() + "</td>")


        // add table to dom
        $table.appendTo('#dynamicTable');

        ResetMedControl();
        ShowMedSection('addMed', 'Add');
    }
    else {
        ModifiedTable.rows(1).cells(0).attributes["Mitem"].nodeValue = MedName;

        ModifiedTable.rows(1).cells(0).attributes["Mdos"].nodeValue = MedDosages;

        ModifiedTable.rows(1).cells(0).attributes["MdosUnit"].nodeValue = MedDosUnit;

        if ($('#Med_DosageUnit>option:selected').text() != "Choose One") {
            ModifiedTable.rows(1).cells(0).innerText = $(".Med_Name").val() + " " + $(".Med_Dosage").val() + " " + $('#Med_DosageUnit>option:selected').text();
        }
        else {
            ModifiedTable.rows(1).cells(0).innerText = $(".Med_Name").val() + " " + $(".Med_Dosage").val();
        }

        if ($('#Med_Frequency>option:selected').text() != "Choose One") {
            if (ModifiedTable.rows.length == 3) {
                var newrow = document.getElementById(ModifiedTable.id).insertRow(2);
                var newcell = newrow.insertCell(0)
                newcell.setAttribute("Mitem");
            }
            ModifiedTable.rows(2).cells(0).attributes["Mitem"].nodeValue = Freq;
            ModifiedTable.rows(2).cells(0).innerText = "Frequency - " + $('#Med_Frequency>option:selected').text();
        }
        else {

            if (ModifiedTable.rows.length == 4) {
                document.getElementById(ModifiedTable.id).deleteRow(2);
            }
        }

        if (ModifiedTable.rows.length == 4) {
            ModifiedTable.rows(3).cells(0).attributes["Mitem"].nodeValue = MedStatus;
            ModifiedTable.rows(3).cells(0).innerText = "Status - " + $('#Med_Status>option:selected').text();
        }
        else if (ModifiedTable.rows.length == 3) {
            ModifiedTable.rows(2).cells(0).attributes["Mitem"].nodeValue = MedStatus;
            ModifiedTable.rows(2).cells(0).innerText = "Status - " + $('#Med_Status>option:selected').text();
        }
        PatientMedication = "Medication~Name:" + MedName + ";Status:" + MedStatus + "";
        //
        if (MedDosages != "" && MedDosages != undefined) {
            PatientMedication = PatientMedication + "Dosage:" + MedDosages + "";
        }

        if (MedDosUnit != "" && MedDosUnit != undefined) {
            PatientMedication = PatientMedication + "Dosage~Unit:" + MedDosUnit + "";
        }

        if (Freq != "" && Freq != undefined) {
            PatientMedication = PatientMedication + "Frequency:" + Freq + "";
        }

        ModifiedTable.attributes["PatientMed"].nodeValue = PatientMedication;

        ResetMedControl();
        ModifiedTable = undefined;
        ShowMedSection('addMed', 'Add');
    }
}
var ModifiedTable;

function ResetMedControl() {
    $(".Med_Name").val("");
    $(".Med_Dosage").val("");

    $('#Med_DosageUnit').val('Choose One').attr('selected', 'selected');
    $('#Med_Frequency').val('Choose One').attr('selected', 'selected');
    $('#Med_Status').val('Choose One').attr('selected', 'selected');
}

function EditMedication(EdittableID) {
    if (document.getElementById('addMed').style.display == "none") {
        ShowMedSection('addMed', 'Edit');
    }
    var MediciationName = EdittableID.rows(1).cells(0).attributes["Mitem"].nodeValue;
    MediciationName = MediciationName.replace(/\~/g, ' ');
    var Dosage = EdittableID.rows(1).cells(0).attributes["Mdos"].nodeValue;
    Dosage = Dosage.replace(/\~/g, ' ');
    var DosageUnit = EdittableID.rows(1).cells(0).attributes["MdosUnit"].nodeValue;
    DosageUnit = DosageUnit.replace(/\~/g, ' ');
    var Frequency = undefined;
    if (EdittableID.rows.length == 4) {
        Frequency = EdittableID.rows(2).cells(0).attributes["Mitem"].nodeValue;
        Frequency = Frequency.replace(/\~/g, ' ');
    }
    var Status;
    if (EdittableID.rows.length == 4) {
        Status = EdittableID.rows(3).cells(0).attributes["Mitem"].nodeValue;
        Status = Status.replace(/\~/g, ' ')
    }
    else if (EdittableID.rows.length == 3) {
        Status = EdittableID.rows(2).cells(0).attributes["Mitem"].nodeValue;
        Status = Status.replace(/\~/g, ' ')
    }



    $(".Med_Name").val(MediciationName);

    if (Dosage != "undefined" && Dosage != "") {
        $(".Med_Dosage").val(Dosage);
    }

    if (DosageUnit != "" && DosageUnit != "undefined") {
        $('#Med_DosageUnit').val(DosageUnit).attr('selected', 'selected');
    }
    if (Frequency != "" && Frequency != undefined) {
        $('#Med_Frequency').val(Frequency).attr('selected', 'selected');
    }
    $('#Med_Status').val(Status).attr('selected', 'selected');

    ModifiedTable = EdittableID;



}
var NewID = 0;
var NewTableID = 0;
function DeleteMedication(DeltableId) {
    var deltable_ID = "#" + DeltableId.id;
    $("#" + DeltableId.id).remove();

    NewID = 0;

    $("#dynamicTable table").each(function (i) {
        NewID = NewID + 1;

        NewTableID = "table" + NewID;
        this.rows(0).cells(0).children(1).firstChild.attributes["id"].nodeValue = "Edit" + NewID;
        this.rows(0).cells(0).children(1).firstChild.attributes["onclick"].nodeValue = "EditMedication(" + NewTableID + ");return false;";
        this.rows(0).cells(0).children(2).firstChild.attributes["id"].nodeValue = "Delete" + NewID;
        this.rows(0).cells(0).children(2).firstChild.attributes["onclick"].nodeValue = "DeleteMedication(" + NewTableID + ");return false;";
        this.rows(0).cells(0).children(0).innerText = "Medication #" + NewID;
        this.id = NewTableID;
    });

}

function ShowHideGenderSection(PatientGender) {
    //    FemaleQues
    //    MaleQues
    if (PatientGender == "Male") {
        document.getElementById("MaleQues").style.display = "block";
    }
    else if (PatientGender == "Female") {
        document.getElementById("FemaleQues").style.display = "block";
    }
    else if (PatientGender == "Other") {
    }
}

function CheckSubmit() {

    for (var i = pfselection.length - 1; i >= 0; i--) {
        var isdt = "0";
        isdt = isdatatable(pfselection[i].GroupId);
        if (isdt === "0") {
            pfselection.splice(i, 1);
        }
    }

    if (getPfselection(false, 0) === true) {

        $('#modalHistoryConfirmation').modal();
    }
}

function ValidateForm() {
    $('#modalValidateForm').modal();
}
function RedirectToHistory() {
    //document.location.href = "../../DynamicHeathFormList.html";
}
function RedirectToHealth() {
    document.location.href = "healthforms.html";
}

function SubmitHistory() {
    //    var msg2 = { "SerachRequest": { "SessionId": sessionStorage.PatientPortalSessionID, "PracticeId": globalPracticeID },
    //        "crt": { "data": Result, "patientid": sessionStorage.LoggedInUserID }

    //    };

    //    Data = JSON.stringify(msg2);
    //    ContentType = "application/json; charset=utf-8";
    //    DataType = "json"; ProcessData = true;
    //    method = "SubmitForm";
    //    CallService();

    try {
        ShowProgressbarwithOverlay();
        var practid = $.rc4EncryptStr(globalausid, globalkey);
        var nPRId = $.rc4EncryptStr(sessionStorage.nPRId, globalkey);
        var tempdata = Resultdata.join("#$rp!bv*kf%us$#");

        var dtActivityDateTime = dateToWcf($.now());

        var msg = { "SearchRequest": { "appCodeField": globalAppCodeField, "practiceIDField": practid, "vendorIDField": globalVendorID, "SessionId": sessionStorage.PatientPortalSessionID },
            "crt": { "data": tempdata, "patientid": sessionStorage.LoggedInUserID, "PRId": nPRId, "ActivityDateTime": dtActivityDateTime, "PflistID": sessionStorage.patformid }

        };
        Type = "POST";
        Url = globalServiceURL + "PutHistory";


        Data = JSON.stringify(msg);
        ContentType = "application/json; charset=utf-8";
        DataType = "json"; ProcessData = true;
        method = "SubmitForm";

        CallService();
    } catch (e) { HideProgressbarwithOverlay(); }

}
function ServiceSucceeded(result) {
    if (DataType == "json") {
        if (method == "SubmitForm") {
            if (result.JsonAddHistoryResult.descriptionField == "Success") {
                HideProgressbarwithOverlay();
                $('#modalHealthSuccssed').modal();
            }
            else {
                HideProgressbarwithOverlay();
                $('#ErrMsg').html("Failed to submit patient form. Please try again.");
                $('#modalHealthForm').modal();
            }
        }
        else if (method == "GetHelathformData") {
            if (result.JsonPortalGetDynamicHealthformdataResult.descriptionField.toLowerCase() == "success") {
                FillData(result.JsonPortalGetDynamicHealthformdataResult.resultRecordsField);
                HideProgressbarwithOverlay();
            }
            else {
                HideProgressbarwithOverlay();
                $('#ErrMsg').html("Failed to Load patient form data. Please try again.");
                $('#modalHealthForm').modal();
            }
        }
    }
}

function DisabledCheckbox(TblName, ChkName) {
    $("input:checkbox").uniform();
    var isChecked = $('#' + ChkName).is(':checked');
    if (isChecked == true) {

        $('#' + TblName + ' input[type="checkbox"], select').prop("disabled", true);
        $('#' + ChkName + ', select').prop("disabled", false);

        $('#' + TblName + ' input[type="checkbox"]').each(function () {
            if (this.id != ChkName) {
                this.checked = false;
                $.uniform.update(this);
            }
        });

        if (TblName == 'tblVaccines' && ChkName == 'a_None') {
            document.getElementById('txtMeningitis').style.display = "none";
            document.getElementById('txtChickenpox').style.display = "none";
            document.getElementById('txtHepatitisA').style.display = "none";
            document.getElementById('txtHepatitisB').style.display = "none";
            document.getElementById('txtHPV').style.display = "none";
            document.getElementById('txtInfluenza').style.display = "none";
            document.getElementById('txtPneumonia').style.display = "none";
            document.getElementById('txtShingles').style.display = "none";
            document.getElementById('txtTetanus').style.display = "none";
            document.getElementById('txtGardasil').style.display = "none";
        }
    }
    else {
        $('#' + TblName + ' input[type="checkbox"], select').prop("disabled", false);
        $('#' + ChkName + ', select').prop("disabled", false);
    }




}
