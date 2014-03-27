function LikePhoto(photoId, userName) {
    $.ajax({
        type: "POST",
        url: "/Photo/Like",
        data: { userName: userName, photoId: photoId },
    }).done(function() {
        var num = parseInt($("#number-likes").text());
        $("#number-likes").text(num + 1);
    });
}