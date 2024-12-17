const uriClothes = "http://localhost:5085/myClothes";
const uriUser = "http://localhost:5085/myUser";
//const uri = '..Tasks' אפשר גם:
let clothes = [];
userName = document.getElementById('userName')

function checkToken() {
    fetch(uriClothes, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
        })
        .then(response => response.json())
        .then(getItems())
        .catch(error => {
            sessionStorage.setItem("check", error)
            console.log(error);
            location.href = "./login.html"

        });

}

function getItems() {
    fetch(uriClothes, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
        })
        .then(response => response.json())
        .then(data => _displayItems(data))
        // .catch(error => console.error('Unable to get items.', error));
        .catch(error => {
            console.log(error);
            // location.href = "./login.html"

        });

}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addcolorTextbox = document.getElementById('add-color');
    const isShabatTextbox = document.getElementById('is-shabat');

    const item = {
        isShabatTextbox: isShabatTextbox.checked,
        name: addNameTextbox.value.trim(),
        color: addcolorTextbox.value.trim()
    };

    fetch(uriClothes, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addcolorTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uriClothes}/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = clothes.find(item => item.id === id);
    console.log(id);
console.log(item);
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-color').value = item.color;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isShabat').checked = item.isShabatTextbox;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isDone: document.getElementById('edit-isShabat').checked,
        name: document.getElementById('edit-name').value.trim(),
        color: document.getElementById('edit-color').value.trim()
    };

    fetch(`${uriClothes}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(item)
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput('editForm');

    return false;
}

function closeInput(formToClose) {
    document.getElementById(formToClose).style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'tasks' : 'tasks kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('cloths');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isShabatTextbox = document.createElement('input');
        isShabatTextbox.type = 'checkbox';
        isShabatTextbox.disabled = true;
        isShabatTextbox.checked = item.isShabatTextbox;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isShabatTextbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td5 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.color);
        td5.appendChild(textNode2);
        

        let td3 = tr.insertCell(3);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(4);
        td4.appendChild(deleteButton);
     
    });

    clothes = data;
}



// if (localStorage.getItem("token") == null) {
//     console.log("login");
//     sessionStorage.setItem("not", "not exist token")

//     location.href = "./login.html"

// }

function createLink() {
    if (localStorage.getItem("link") == "true") {

        let link = document.createElement("a");
        link.href = "./userList.html";
        link.innerHTML = "users";
        console.log(sessionStorage.getItem("link"));
        document.body.appendChild(link);
    }
}
console.log(localStorage.getItem("token"));


function editUser() {

    document.getElementById('edit-name-user').value = user.name
    document.getElementById('edit-id-user').value = user.id;
    document.getElementById('edit-password-user').value = user.password;
    document.getElementById('editUserForm').style.display = 'block';
    console.log(document.getElementById('edit-id-user').value);
}

function updateUser() {
    const newUser = {
        id: user.id,
        name: document.getElementById('edit-name-user').value.trim(),
        password: document.getElementById('edit-password-user').value.trim()
    };
    fetch(`${uriUser}/${user.id}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify(newUser)
        })
        .then(() => {
            user = newUser;
            userName.innerHTML = user.name;
        })
        .catch(error => console.error('Unable to update item.', error));

    closeInput('editUserForm')
    return false;
}

function createUser(response) {
    user = response;
    userName.innerHTML = user.name;

}

function getUser() {
    const userId = localStorage.getItem("userId");
    fetch(`${uriUser}/${userId}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
        })
        .then(response => response.json())
        .then(response => createUser(response))
        .catch(error =>
            console.log(error));

}

let user;
getUser()
getItems();
createLink()