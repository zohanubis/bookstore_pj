document.addEventListener('DOMContentLoaded', () => {
    const signUpButton = document.getElementById('signUp');
    const signInButton = document.getElementById('signIn');
    const container = document.getElementById('container');
    //Change
    signUpButton.addEventListener('click', () => {
        container.classList.add('right-panel-active');
    });

    signInButton.addEventListener('click', () => {
        container.classList.remove('right-panel-active');
    });
    const signUpForm = document.querySelector('.sign-up-container form');
    const signInForm = document.querySelector('.sign-in-container form');

    const nameInput = document.getElementById('nameInput');
    const emailInput = document.getElementById('emailInput');
    const passwordInput = document.getElementById('passwordInput');
    const signInEmailInput = document.getElementById('signInEmailInput');
    const signInPasswordInput = document.getElementById('signInPasswordInput');
    //Error message
    const nameError = document.getElementById('nameError');
    const emailError = document.getElementById('emailError');
    const passwordError = document.getElementById('passwordError');
    const signInEmailError = document.getElementById('signInEmailError');
    const signInPasswordError = document.getElementById('signInPasswordError');

    signUpForm.addEventListener('submit', (e) => {
        e.preventDefault();

        const nameRegex = /^[a-zA-Z\s]+$/;
        const emailRegex = /^\S+@\S+\.\S+$/;
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/;

        // Reset error messages
        nameError.textContent = '';
        emailError.textContent = '';
        passwordError.textContent = '';

        // Validate name
        if (!nameInput.value || !nameRegex.test(nameInput.value)) {
            nameError.textContent = 'Please enter a valid name';
            return;
        }

        // Validate email
        if (!emailInput.value || !emailRegex.test(emailInput.value)) {
            emailError.textContent = 'Please enter a valid email address';
            return;
        }

        // Validate password
        if (!passwordInput.value || !passwordRegex.test(passwordInput.value)) {
            passwordError.textContent =
                'Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number';
            return;
        }

        // Display success message
        const signUpSuccessMessage = document.getElementById('signUpSuccessMessage');
        signUpSuccessMessage.textContent = 'Sign up successful!';
        signUpSuccessMessage.classList.add('success');

        // Clear form inputs
        signUpForm.reset();

        // Redirect to home page
        window.location.href = 'index.html';
    });

    signInForm.addEventListener('submit', (e) => {
        e.preventDefault();

        const emailRegex = /^\S+@\S+\.\S+$/;
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/;

        // Reset error messages
        signInEmailError.textContent = '';
        signInPasswordError.textContent = '';

        // Validate email
        if (!signInEmailInput.value || !emailRegex.test(signInEmailInput.value)) {
            signInEmailError.textContent = 'Please enter a valid email address';
            return;
        }

        // Validate password
        if (!signInPasswordInput.value || !passwordRegex.test(signInPasswordInput.value)) {
            signInPasswordError.textContent =
                'Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number';
            return;
        }

        // Display success message
        const signInSuccessMessage = document.getElementById('signInSuccessMessage');
        signInSuccessMessage.textContent = 'Sign in successful!';
        signInSuccessMessage.classList.add('success');

        // Clear form inputs
        signInForm.reset();

        // Redirect to home page
        window.location.href = 'index.html';
    });
});
