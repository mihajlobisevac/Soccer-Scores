
var hamClosed = document.getElementById("hamClosed")
var hamOpened = document.getElementById("hamOpened")
var sideMenu = document.getElementById("sideMenu")
var sideMenuBG = document.getElementById("sideMenuBG")

function openSideMenu() {
    hamClosed.style.display = "none"
    hamOpened.style.display = "block"
    sideMenu.style.display = "block"
    sideMenuBG.style.display = "block"
}

function closeSideMenu() {
    hamClosed.style.display = "block"
    hamOpened.style.display = "none"
    sideMenu.style.display = "none"
    sideMenuBG.style.display = "none"
}
