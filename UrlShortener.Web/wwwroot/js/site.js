$(function () {
    $("#short-btn").click(function () {
        $("#outputInfo").empty();
        let originalUrl = document.getElementById("url").value.trim();
        let data = JSON.stringify({
            "originalLink": originalUrl,
        });
        $.ajax({
            url: '/api/url/get',
            data: data,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (resp) {
                var shortLink = window.location.origin + "/" + resp.shortLink;

                var shortLinkElement = $("<a>", { 'class': "mr-2", 'href': shortLink, 'text': shortLink, 'id': "short-link" });
                var labelShortLink = $("<label>", { 'class': "mr-2", 'text': "Short link:" });
                var buttonCopyShortLink = $("<button>", { 'class': "btn btn-primary btn-sm", 'text': "Copy short link", 'id': "copy-link" });
                var formShortLink = $("<div>", { 'class': "form-inline mb-2" });

                formShortLink
                    .append(labelShortLink)
                    .append(shortLinkElement)
                    .append(buttonCopyShortLink);

                var labelOriginalLink = $("<label>", { 'class': "mr-2", 'text': "Original link:" });
                var originalLink = $("<a>", { 'class': "mr-2", 'href': resp.originalLink, 'text': resp.originalLink });
                var formOriginalLink = $("<div>", { 'class': "form-inline" });

                formOriginalLink
                    .append(labelOriginalLink)
                    .append(originalLink);

                $("#outputInfo")
                    .append(formShortLink)
                    .append(formOriginalLink);
            },

            error: function () {
                $("#outputInfo")
                    .append("<p>The URL is not valid, make sure the URL you tried to shorten is correct.</p>")
                    .append("<p>Possible errors:</p>")
                    .append("<ul><li>Check if the domain is correct</li><li>Check if the site is online</li><li>Check the length of the url is not too small</li><li>Check the address bars and punctuation</li> <li>The URL may have been blocked</li><li>The url may have been reported</li></ul>")
            }
        });
    });
    $(document).on("click", "#copy-link", function () {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(document.getElementById("short-link").href).select();
        document.execCommand("copy");
        $temp.remove();
    });
});