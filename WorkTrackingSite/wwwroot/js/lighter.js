i = 0;
dt = new Array("0000A0", "A00000", "00A000", "00A0A0", "A000A0", "A0A000");

function changeColor() {
    document.getElementById('descriptionMessage').style.color = '#' + dt[i++];
    if (i >= dt.length) i = 0;
}

setInterval(next_cl, 500);

document.getElementById('uploadB').onclick = changeColor();