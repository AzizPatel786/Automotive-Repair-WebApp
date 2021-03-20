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


let mainNav = document.getElementById("js-menu");
let navBarToggle = document.getElementById("js-navbar-toggle");

navBarToggle.addEventListener("click", function () {
    mainNav.classList.toggle("active");
});


