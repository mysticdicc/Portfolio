function scrollResponse() {
    var div = document.getElementById('blobber');
    var layout = document.getElementById('layout');

    var layoutY = layout.scrollTop;
    div.style.top = layoutY - 50  + 'px';
}