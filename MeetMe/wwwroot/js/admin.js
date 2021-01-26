﻿
var previewImageInitialSrc = null;
var previewImageInitialVisible = null;

//data-preview-image-target="img-id"
$("[data-preview-image-target]").on("input", function (event) {
    var input = this;
    var targetImg = $(this).data("preview-image-target");
    var img = $(targetImg)[0];
    if (previewImageInitialVisible == null) {
        previewImageInitialVisible = img.style.display != "none";
    }
    if (previewImageInitialSrc == null) {
        previewImageInitialSrc = img.src;
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        //render okumayı bitirdiğinde
        reader.onload = function (e) {
            //okunan resmi <img...> elementi üzerinde göster
            img.src = e.target.result;
            img.style.display = "inline";
        };

        reader.readAsDataURL(input.files[0]);
    }
    else {
        if (previewImageInitialVisible) {
            img.src = previewImageInitialSrc;
        }
        else {
            img.style.display = "none";
        }
    }
});