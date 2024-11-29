function editUser1() {
    // Get the clicked button
    const button = event.target;
    const row = button.closest("tr"); // Get the closest row
    const cells = row.getElementsByTagName("td"); // Get all table cells in the row

    // Prepare the data object from row values
    const userId = row.querySelector(".user-checkbox").value;
    const firstName = cells[1].innerText.trim();
    const lastName = cells[2].innerText.trim();
    const dob = cells[3].innerText.trim();
    const email = cells[4].innerText.trim();
    const roles = cells[5].innerText.trim();

    // Log or display the extracted data for debugging
    console.log({
        userId,
        firstName,
        lastName,
        dob,
        email,
        roles,
    });

    const editFormHtml = redirectEditForm(userId, firstName, lastName, dob, email, roles);
    // Construct an edit form dynamically (or pass these values elsewhere)
    //const editFormHtml = `
    //    <div id="editFormContainer">
    //        <form id="editUserForm">
    //            <input type="hidden" id="userId" name="userId" value="${userId}" />
    //            <div>
    //                <label for="FirstName">First Name:</label>
    //                <input type="text" id="FirstName" name="FirstName" value="${firstName}" />
    //            </div>
    //            <div>
    //                <label for="LastName">Last Name:</label>
    //                <input type="text" id="LastName" name="LastName" value="${lastName}" />
    //            </div>
    //            <div>
    //                <label for="DOB">Date of Birth:</label>
    //                <input type="date" id="DOB" name="DOB" value="${dob}" />
    //            </div>
    //            <div>
    //                <label for="Email">Email:</label>
    //                <input type="email" id="Email" name="Email" value="${email}" />
    //            </div>
    //            <div>
    //                <label for="Roles">Role:</label>
    //                <input type="text" id="Roles" name="Roles" value="${roles}" />
    //            </div>
    //            <button type="submit">Update</button>
    //        </form>
    //    </div>
    //`;

    // Insert the form into the page (e.g., below the table)
    const tableGroup = document.querySelector(".table-group");
    if (!document.getElementById("editFormContainer")) {
        tableGroup.insertAdjacentHTML("afterend", editFormHtml);
    }

    // Add a submit listener for the form
    const editUserForm = document.getElementById("editUserForm");
    editUserForm.addEventListener("submit", function (event) {
        event.preventDefault();

        // Gather updated values from the form
        const updatedData = {
            userId: document.getElementById("userId").value,
            FirstName: document.getElementById("firstname").value,
            LastName: document.getElementById("lastname").value,
            DOB: document.getElementById("dob").value,
            Email: document.getElementById("email").value,
            Roles: document.getElementById("roles").value,
        };

        // Send the updated data to the server
        fetch('/api/UserDetails/UpdateUser', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(updatedData),
        })
            .then((response) => {
                if (response.ok) {
                    alert("User updated successfully!");
                    location.reload(); // Refresh the page to reflect updates
                } else {
                    return response.json().then((error) => {
                        throw new Error(error.message || "Error updating user");
                    });
                }
            })
            .catch((error) => {
                console.error("Error:", error);
                alert("An error occurred while updating the user.");
            });
    });
}


function editUser() {
    // Get the clicked button
    const button = event.target;
    const row = button.closest("tr"); // Get the closest row
    const cells = row.getElementsByTagName("td"); // Get all table cells in the row

    // Prepare the data object from row values
    const userId = row.querySelector(".user-checkbox").value;
    const firstName = cells[1].innerText.trim();
    const lastName = cells[2].innerText.trim();
    const dob = cells[3].innerText.trim();
    const email = cells[4].innerText.trim();
    const roles = cells[5].innerText.trim();

    // Log or display the extracted data for debugging
    console.log({
        userId,
        firstName,
        lastName,
        dob,
        email,
        roles,
    });

    // Redirect to the edit form page
    redirectEditForm(userId, firstName, lastName, dob, email, roles);
}

function search() {
    const searchElement = document.getElementById("first").value;
    const url = `/api/UserDetails/searchUser?firstname=${encodeURIComponent(searchElement)}`;
    window.location.href = url;
}

function redirectEditForm(userId, firstName, lastName, dob, email, roles) {

    const url = `/UserDetails/EditUser?userId=${encodeURIComponent(userId)}&firstname=${encodeURIComponent(firstName)}&lastname=${encodeURIComponent(lastName)}&dob=${encodeURIComponent(dob)}&email=${encodeURIComponent(email)}&roles=${encodeURIComponent(roles)}`;
    window.location.href = url;

}

function updateUser() {
    // Add a submit listener for the form
    const editUserForm = document.getElementById("editUserForm");
    editUserForm.addEventListener("submit", function (event) {
        event.preventDefault();

        // Gather updated values from the form
        const updatedData = {
            userId: document.getElementById("userId").value,
            FirstName: document.getElementById("firstName").value,
            LastName: document.getElementById("lastName").value,
            DOB: document.getElementById("dob").value,
            Email: document.getElementById("email").value,
            Roles: document.getElementById("roles").value,
        };

        // Send the updated data to the server
        fetch('/api/UserDetails/UpdateUser', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(updatedData),
        })
            .then((response) => {
                if (response.ok) {
                    alert("User updated successfully!");
                    redirectToHome();
                   
                } else {
                    return response.json().then((error) => {
                        throw new Error(error.message || "Error updating user");
                    });
                }
            })
            .catch((error) => {
                console.error("Error:", error);
                alert("An error occurred while updating the user.");
            });
    });
}

function redirectToHome() {
    const url = '/UserDetails/UserScreen'
    window.location.href = url;
    location.reload(); // Refresh the page to reflect updates
}