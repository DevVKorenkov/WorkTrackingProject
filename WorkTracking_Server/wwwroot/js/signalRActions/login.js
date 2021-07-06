window.checkAccess = () => {
    window.hubConnection.on("AccessDenide", () => alert('Доступ запрещен. Обратитесь к руководителю или в отдел технической поддержки'));

    window.hubConnection.on("GiveUsers", (comboboxes) => {
        window.comboboxes = comboboxes;
        window.chouseUser();
    });

    window.hubConnection.on("GiveAll", (mainObject) => {
        window.mainObject = mainObject;
        window.display();
    });

    window.hubConnection.invoke("StartGetUsers");
}