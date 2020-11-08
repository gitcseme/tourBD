
$(document).ready(function () {
    $('#filePhoto').change(function () {

        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = (e) => {
                $('#cover-photo').attr('src', e.target.result);
            };

            reader.readAsDataURL(this.files[0]);
        }

    })
});