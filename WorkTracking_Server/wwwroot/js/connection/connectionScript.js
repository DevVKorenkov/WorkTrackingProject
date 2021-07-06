window.addEventListener('DOMContentLoaded', async () => {
    const hubConnection = new signalR.HubConnectionBuilder().withUrl("/TrackingServer").build();
    await hubConnection.start();
    window.hubConnection = hubConnection;
    window.checkAccess();

    document.getElementById("enterButton").addEventListener("click", () => {
        let user = document.getElementById("loginAdmin").value;
        window.hubConnection.invoke("RunCheckAccess", user);
    });
});