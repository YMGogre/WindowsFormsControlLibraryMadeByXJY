var colors = ["#C8C9CC", "#409EFF", "#67C23A", "#E6A23C", "#F56C6C"];
var i = 0;
setInterval(function() {
    document.getElementById('circle').style.backgroundColor = colors[i];
    i = (i + 1) % colors.length;
}, 1000);