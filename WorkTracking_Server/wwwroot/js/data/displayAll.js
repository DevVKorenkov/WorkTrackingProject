window.display = () => {
    var works = window.mainObject.adminWorks;
    var body = document.body;
    var table = document.createElement('table');

    for (w of works) {
        var tempTr = document.createElement('tr');
        for (t in w) {
            var tempTd = document.createElement('td');
            tempTd.style.border = '1px solid black'
            tempTr.appendChild(tempTd);
            tempTd.innerText = w[t];
        }
        table.appendChild(tempTr);
    }
    body.appendChild(table)
};