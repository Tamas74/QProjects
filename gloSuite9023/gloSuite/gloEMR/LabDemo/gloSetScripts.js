// JavaScript Document


function SlideStart() {
    t = setTimeout("slideit()", 5000);
}

function getName(s)
 {
    var d = s.lastIndexOf('.');
    return s.substring(s.lastIndexOf('/') + 1, d < 0 ? s.length : d);
}


function ShowSelected(sentVar) {

    if (getName(document.getElementById('slide').src) == "print") {
        if (sentVar == 11) {
            alert("Lab Requisition sent to printing");
        }
        else
        document.getElementById('slide').src = "Images/print.png";
    }
    else {
        if (sentVar == 1) {
            if (getName(document.getElementById('slide').src) == "CBC") {
                document.getElementById('slide').src = "Images/launch.png";
            }
            else
                document.getElementById('slide').src = "Images/CBC.PNG";
        }

        if (sentVar == 2) {
            document.getElementById('slide').src = "Images/GLU.JPG";
        }

        if (sentVar == 3) // GLU 
        {
            if (getName(document.getElementById('slide').src) == "GLUCOSE") {
                document.getElementById('slide').src = "Images/CBC.PNG";
            }
            else
                document.getElementById('slide').src = "Images/GLUCOSE.PNG";
        }

        if (sentVar == 4) // 780
        {
            if (getName(document.getElementById('slide').src) == "780") {
                document.getElementById('slide').src = "Images/006.PNG";
            }
            else
                document.getElementById('slide').src = "Images/780.PNG";
        }

        if (sentVar == 5) {//006
            if (getName(document.getElementById('slide').src) == "006") {
                document.getElementById('slide').src = "Images/CBC.PNG";
            }
            else
                document.getElementById('slide').src = "Images/006.PNG";
        }


        if (sentVar == 6) // Lab List
        {
            if (getName(document.getElementById('slide').src) == "Labs") {
                document.getElementById('slide').src = "Images/Launch.png";
            }
            else
                document.getElementById('slide').src = "Images/Labs.PNG";
        }

        if (sentVar == 7) // Billing Type
        {
            if (getName(document.getElementById('slide').src) == "Client") {
                document.getElementById('slide').src = "Images/Launch.png";
            }
            else
                document.getElementById('slide').src = "Images/Client.PNG";
        }
        
        if (sentVar == 8) // Add to List
        {
            document.getElementById('slide').src = "Images/Add.png";
        }
        if (sentVar == 9) // Remove
        {
            document.getElementById('slide').src = "Images/Launch.png";
        }

        if (sentVar == 10) // Remove
        {
            if (getName(document.getElementById('slide').src) == "Add") {
                document.getElementById('slide').src = "Images/print.png";
            }
            else
                alert("Please add at least one test to validate")
        }
        if (sentVar == 12) // Remove
        {
            if (getName(document.getElementById('slide').src) == "Grp") {
                document.getElementById('slide').src = "Images/Launch.png";
            }
            else
                document.getElementById('slide').src = "Images/Grp.PNG";
        }
    }

}



