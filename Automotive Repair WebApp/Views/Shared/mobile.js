//const doc = document;
//const menuOpen = doc.querySelector(".menu");
//const menuClose = doc.querySelector(".close");
//const overlay = doc.querySelector(".overlay");

//menuOpen.addEventListener("click", () => {
//    overlay.classList.add("overlay--active");
//});

//menuClose.addEventListener("click", () => {
//    overlay.classList.remove("overlay--active");
//});


let mainNav = document.getElementById('main-nav');
let navbarToggle = document.getElementById('navbar-toggle');



navbarToggle.addEventListener('click', function () {

    if (this.classList.contains('active')) {
        mainNav.style.display = "none";
        this.classList.remove('active');
    }
    else {
        mainNav.style.display = "flex";
        this.classList.add('active');

    }
});








