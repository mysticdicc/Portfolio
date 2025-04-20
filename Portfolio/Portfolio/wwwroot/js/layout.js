function addEventListener() {
    document.getElementById('layout').addEventListener('scroll', scrollResponse);
}

function scrollResponse() {
    var blobber = document.getElementById('blobber');
    var layout = document.getElementById('layout');
    var body = document.getElementById('body');

    var window = window;

    var layoutY = layout.scrollTop;
    var blobberTop = blobber.style.top;
    var blobberTopInt = parseFloat(blobberTop);
    var pos = blobber.clientHeight + blobberTopInt;

    if (body.clientHeight >= pos || blobberTop == '')
    {
        blobber.style.top = layoutY + 10 + 'px';
    }
}