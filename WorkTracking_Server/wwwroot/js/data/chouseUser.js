window.chouseUser = () => {
    var select = document.getElementById('loginAdmin');
    var users = window.comboboxes.adminsList;

    for (var i = 0; i < users.length; i++) {
        var user = users[i];
        var el = document.createElement("option");
        el.textContent = user;
        el.value = user;
        select.appendChild(el);
    }
}