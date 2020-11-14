
function CommentShower(btn) {
    let text = btn.innerText;
    $(btn)[0].children[0].innerText = (text == "view comments" ? "hide comments" : "view comments");

    $(btn).siblings('.comment-container').toggle();
}

function AjaxComment(btn, postId, folderPath, imageUrl, name) {
    let box = $(btn).parent().siblings('.comment-box').children()[0];
    let message = box.value;
    box.value = "";
    if (!imageUrl.includes(folderPath))
        imageUrl = folderPath + imageUrl;

    $.ajax({
        url: '/Forum/AddComment',
        type: 'POST',
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { postId: postId, message: message },
        success: function (response) {
            let commentHtml = `<div class="row">
                                                <div class="comment-autor-pic col-auto">
                                                    <img src="`+ imageUrl + `" alt="profile-pic" id="author-profile-pic" /> <span class="commment-user-name">` + name + `</span>
                                                </div>
                                                <div class="col">
                                                    <p>`+ message + `</p>
                                                </div>
                                            </div>`;

            $(btn).parent().parent().siblings('.comment-container').append(commentHtml);
        },
        error: function (data, status, xhr) {
            console.log(status);
        }
    });
}

let cmtId = '', imgUrl = '', fName = '';
function CreateReplayBox(item, commentId, folderPath, imageUrl, name) {
    cmtId = commentId;
    imgUrl = imageUrl;
    fName = name;
    if (!imageUrl.includes(folderPath))
        imageUrl = folderPath + imageUrl;

    let replayHtml = `<div class="row default-replay-box">
                                        <div class="replay-author-pic col-auto offset-1">
                                            <img src="` + imageUrl + `" alt="profile-pic" id="author-profile-pic" /> <span class="commment-user-name">` + name + `</span>
                                        </div>
                                        <div class="comment-box col">
                                            <textarea id="default-comment-box" name="cmtbox" rows="1"></textarea>
                                        </div>
                                        <div class="col-auto pl-1">
                                            <button id="btn-add-comment" class="btn btn-sm btn-outline-primary" onclick="AjaxReplay(this, '` + cmtId + `', '` + folderPath + `', '` + imgUrl + `', '` + fName + `')">
                                                <i class="fa fa-plus-circle" aria-hidden="true"> Replay</i>
                                            </button>
                                        </div>
                                    </div>`;

    $(item).parent().parent().parent().parent().siblings('.replay-container').append(replayHtml);
}

function AjaxReplay(item, commentId, folderPath, imageUrl, name) {
    if (!imageUrl.includes(folderPath))
        imageUrl = folderPath + imageUrl;
    let message = $(item).parent().siblings('.comment-box').children()[0].value;

    $.ajax({
        url: '/Forum/AddReplay',
        type: 'POST',
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { commentId: commentId, message: message },
        success: function (response) {
            let replayHtml = `<div class="row">
                                                <div class="replay-autor-pic col-auto offset-1">
                                                    <img src="`+ imageUrl + `" alt="profile-pic" id="author-profile-pic" /> <span class="commment-user-name">` + name + `</span>
                                                </div>
                                                <div class="col">
                                                    <p>`+ message + `</p>
                                                </div>
                                            </div>`;

            $(item).parent().parent().parent().append(replayHtml);
            $(item).parent().parent().remove(); // delete replay box.
        },
        error: function (data, status, xhr) {
            console.log(status);
        }
    });
}

function countLikesAjax(btn, postId) {

    $.ajax({
        url: '/Forum/AddLike',
        type: 'POST',
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { postId: postId },
        success: function (response) {
            let elem = $(btn).siblings('#like-count');
            let likes = parseInt(elem[0].innerText) + 1;
            elem.text(likes);

            $(btn).addClass('afterLike');
            $(btn).siblings('.afterLikeInstant').addClass('afterLike');
            btn.disabled = true;
        },
        error(data, status, xhr) {
            console.log(status);
        }
    });
}
