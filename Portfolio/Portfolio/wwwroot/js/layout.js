function addEventListener() {
    document.getElementById('layout').addEventListener('scroll', scrollResponse);
}

function scrollResponse() {
    var blobber = document.getElementById('blobber');
    var layout = document.getElementById('layout');
    var body = document.getElementById('body');

    var window = window;

    var blobberTop = blobber.style.top;
    var blobberTopInt = parseFloat(blobberTop);
    var pos = blobber.clientHeight + blobberTopInt;

    var layoutY = layout.scrollTop;

    if (body.clientHeight >= pos || blobberTop == '')
    {
        blobber.style.top = layoutY + 10 + 'px';
    }
    else
    {
        blobber.style.top = (layoutY - 150) + 'px';
    }
}