
// Wait for the DOM to be ready
$(document).ready(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("#loginform").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            Email: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                email: true
            },
            Password: {
                required: true,
            }
        },
        // Specify validation error messages
        messages: {
            Password: "Please provide a password",
            Email: "Please enter a valid email address"
        },
        errorLabelContainer: "#errors",
         wrapper: "li",
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
         submitHandler: function (form) {
             form.submit();
        }
    });

    $("#createform").validate({
        // Specify validation rules
        rules: {
            FirstName: {
                required: true,
            },
            LastName: {
                required: true
            },
            Email: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                email: true
            },
            Password: {
                required: true,
            },

            PasswordReEnter: {
                required: true,
                equalTo: '#Password',
            }
        },
        // Specify validation error messages
        messages: {
            FirstName: "Please provide a first name",
            LastName: "Please provide a last name",
            Password: "Please provide a password",
            PasswordReEnter: {
                required: "Please reenter the password",
                equalTo: "Passwords do not match, reenter!"
            },
            Email: "Please enter a valid email address"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    $("#editform").validate({
        // Specify validation rules
        rules: {
            FirstName: {
                required: true,
            },
            LastName: {
                required: true
            },
            Email: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                email: true
            },
            Password: {
                required: true,
            },

            PasswordReEnter: {
                required: true,
                equalTo: '#Password',
            }
        },
        // Specify validation error messages
        messages: {
            FirstName: "Please provide a first name",
            LastName: "Please provide a last name",
            Password: "Please provide a password",
            PasswordReEnter: {
                required: "Please reenter the password",
                equalTo: "Passwords do not match, reenter!"
            },
            Email: "Please enter a valid email address"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });


});