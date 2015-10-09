
var ajaxFormValidation = (function () {

    function Begin(xhr, status, formName) {

        var form = $("#" + formName);
        $("#" + formName + " input[type = 'submit']").attr('disabled', 'disabled');
        ResetValidation(form);
    }

    function ResetValidation(form) {

        $("#" + form.id + " ul#validation").empty()
        $("#" + form.id + " span[id^='validation-message-']").text("");
        $("#" + form.id).find("input").removeClass("input-validation-error");
        $("#" + form.id).find("select").removeClass("input-validation-error");
    }

    function Clear(self) {

        var form = self.closest("form");

        ResetValidation(form);
        $("#" + form.id + " input[type = 'submit']").removeAttr('disabled');
    }

    function Success(data, status, xhr, formName) {
        //alert(data.valid);

        if (!data.valid) {

            var form = $("#" + formName);

            $.each(data.validationErrors, function (k, v) {

                console.log(v.ReportingLevel);

                $.each(v.ValidationMessages, function (k2, v2) {

                    console.log(v2.Name + " : " + v2.Message);

                    if (v.ReportingLevel == "Model") {
                        $("#" + formName + " ul#validation").append("<li>" + v2.Message + "</li>");
                    }
                    else {
                        $("#" + formName + " input[id*='" + v2.Name + "']").addClass("input-validation-error");
                        $("#" + formName + " select[id*='" + v2.Name + "']").addClass("input-validation-error");

                        $("#" + formName + " #validation-message-" + v2.Name).append(v2.Message);
                    }
                });
            });
        }
    }

    // explicitly return public methods when this object is instantiated
    return {
        Success: Success,
        Clear: Clear,
        Begin: Begin
    };

})();