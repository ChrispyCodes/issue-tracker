﻿

const select = (el, all = false) => {
el = el.trim()
if (all) {
    return [...document.querySelectorAll(el)]
} else {
    return document.querySelector(el)
}
}

/**
 * Easy event listener function
 */
const on = (type, el, listener, all = false) => {
    if (all) {
        select(el, all).forEach(e => e.addEventListener(type, listener))
    } else {
        select(el, all).addEventListener(type, listener)
    }
}

/**
 * Easy on scroll event listener 
 */
const onscroll = (el, listener) => {
    el.addEventListener('scroll', listener)
}

/**
   * Sidebar toggle
   */
if (select('.toggle-sidebar-btn')) {
    on('click', '.toggle-sidebar-btn', function (e) {
        select('body').classList.toggle('toggle-sidebar')
    })
}

/**
 * Navbar links active state on scroll
 */
let navbarlinks = select('#sidebar-nav .scrollto', true)
const navbarlinksActive = () => {
    let position = window.scrollY + 200
    navbarlinks.forEach(navbarlink => {
        if (!navbarlink.hash) return
        let section = select(navbarlink.hash)
        if (!section) return
        if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
            navbarlink.classList.add('active')
        } else {
            navbarlink.classList.remove('active')
        }
    })
}
window.addEventListener('load', navbarlinksActive)
onscroll(document, navbarlinksActive)

/**
   * Toggle .header-scrolled class to #header when page is scrolled
   */
let selectHeader = select('#header')
if (selectHeader) {
    const headerScrolled = () => {
        if (window.scrollY > 100) {
            selectHeader.classList.add('header-scrolled')
        } else {
            selectHeader.classList.remove('header-scrolled')
        }
    }
    window.addEventListener('load', headerScrolled)
    onscroll(document, headerScrolled)
}

/**
   * Autoresize echart charts
   */
const mainContainer = select('#main');
if (mainContainer) {
    setTimeout(() => {
        new ResizeObserver(function () {
            select('.echart', true).forEach(getEchart => {
                echarts.getInstanceByDom(getEchart).resize();
            })
        }).observe(mainContainer);
    }, 200);
}