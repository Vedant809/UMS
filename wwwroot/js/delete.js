function toggleSelectAll() {
    var selectAllCheckbox = document.getElementById("selectAll");
    var checkboxes = document.querySelectorAll(".user-checkbox");
    checkboxes.forEach(function (checkbox) {
        checkbox.checked = selectAllCheckbox.checked;
    });
//    sendUserIdsToAPI(); // Send user IDs whenever selection changes
}

// Function to update the "Select All" checkbox and track selected checkboxes
function updateSelectAll(checkbox) {
    var selectAllCheckbox = document.getElementById("selectAll");
    var checkboxes = document.querySelectorAll(".user-checkbox");

    // Check if all checkboxes are selected
    var allChecked = Array.from(checkboxes).every(function (checkbox) {
        return checkbox.checked;
    });

    // Update the "select all" checkbox state
    selectAllCheckbox.checked = allChecked;
    selectAllCheckbox.indeterminate = !allChecked && Array.from(checkboxes).some(function (checkbox) {
        return checkbox.checked;
    });

    //// Send selected user IDs to the API
    //sendUserIdsToAPI();
}

// Function to collect selected user IDs and send them to the API
function sendUserIdsToAPI() {
    var selectedUserIds = [];
    var checkboxes = document.querySelectorAll(".user-checkbox:checked");

    // Collect UserId values of selected checkboxes
    checkboxes.forEach(function (checkbox) {
        selectedUserIds.push(parseInt(checkbox.value)); // Ensure the values are treated as integers
    });

    // Send to API only if one checkbox is selected (you can modify this logic as needed)
    if (selectedUserIds.length > 0) {
        console.log("Selected UserIds:", selectedUserIds);
        // Call your API here
        deleteUsers(selectedUserIds);
    }
}

// Function to call the API (DELETE request)
function deleteUsers(userIds) {
    var xhr = new XMLHttpRequest();
    var baseUrl = window.location.origin;
    xhr.open("POST", baseUrl+ "/api/UserDetails/DeleteUser", true);
    xhr.setRequestHeader("Content-Type", "application/json");

    
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            alert("Users deleted successfully!");
            // Optionally, refresh the page or update the UI
            location.reload()
            
        }
    };
    xhr.send(JSON.stringify(userIds)); // Send the userIds (as integers) to the API

    console.log("API Endpoint is====>")
    console.log(xhr);
}

// Function to delete selected users
function deleteSelectedUsers() {
    sendUserIdsToAPI(); // Trigger the sending of selected user IDs to the API
}


//Function to call the create user action on the event of button clicking

function submitForm() {
    // Create a JavaScript object with the form data
    var formData = {
        FirstName: document.getElementById('FirstName').value,
        LastName: document.getElementById('LastName').value,
        Email: document.getElementById('Email').value,
        DOB: document.getElementById('DOB').value,
        Roles: document.getElementById('Roles').value
    };

    var baseUrl = window.location.origin;
    // Use @Url.Action in the JavaScript to properly generate the URL
    var actionUrl = baseUrl+'/api/UserDetails/CreateUser';

    // Use fetch to submit the form as JSON
    fetch(actionUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData) // Convert the form data to JSON
    })
        .then(response => {
            if (response.ok) {
                alert("User Added Sucessfully!!");
            }
            if (!response.ok) {
                return response.json().then(errorData => {
                    throw new Error(errorData.message || 'An error occurred');
                });
            }
            return response.json(); // Only attempt to parse JSON if the response is OK
        })
        .then(data => {
            console.log('Success:', data);
            // Handle success (e.g., redirect or show a success message)
            location.reload();
        })
        .catch((error) => {
            console.error('Error:', error);
            // Handle error (e.g., show error message to the user)
        });
}
