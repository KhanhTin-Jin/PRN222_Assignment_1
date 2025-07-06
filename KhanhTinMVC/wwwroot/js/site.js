// FU News Management System - Site JavaScript

$(document).ready(() => {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map((tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl))

    // Auto-hide alerts after 5 seconds
    setTimeout(() => {
        $(".alert").fadeOut("slow")
    }, 5000)

    // Confirm delete functionality
    window.confirmDelete = (id, name, formAction) => {
        $("#deleteItemName").text(name)
        $("#deleteId").val(id)
        $("#deleteForm").attr("action", formAction)
        var modal = new bootstrap.Modal(document.getElementById("deleteConfirmationModal"))
        modal.show()
    }

    // Form validation enhancements
    $("form").on("submit", function () {
        var submitBtn = $(this).find('button[type="submit"]')
        if (submitBtn.length) {
            submitBtn.prop("disabled", true)
            submitBtn.html(
                '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Processing...',
            )
        }
    })

    // Search form enhancements
    $('.search-form input[type="text"]').on("keypress", function (e) {
        if (e.which === 13) {
            // Enter key
            $(this).closest("form").submit()
        }
    })

    // Table row hover effects
    $(".table tbody tr").hover(
        function () {
            $(this).addClass("table-active")
        },
        function () {
            $(this).removeClass("table-active")
        },
    )

    // Auto-resize textareas
    $("textarea")
        .each(function () {
            this.setAttribute("style", "height:" + this.scrollHeight + "px;overflow-y:hidden;")
        })
        .on("input", function () {
            this.style.height = "auto"
            this.style.height = this.scrollHeight + "px"
        })

    // Character counter for text inputs with maxlength
    $("input[maxlength], textarea[maxlength]").each(function () {
        var maxLength = $(this).attr("maxlength")
        var currentLength = $(this).val().length
        var counter = $('<small class="form-text text-muted char-counter">' + currentLength + "/" + maxLength + "</small>")
        $(this).after(counter)

        $(this).on("input", function () {
            var currentLength = $(this).val().length
            $(this)
                .next(".char-counter")
                .text(currentLength + "/" + maxLength)

            if (currentLength > maxLength * 0.9) {
                $(this).next(".char-counter").removeClass("text-muted").addClass("text-warning")
            } else {
                $(this).next(".char-counter").removeClass("text-warning").addClass("text-muted")
            }
        })
    })

    // Smooth scrolling for anchor links
    $('a[href^="#"]').on("click", function (event) {
        var target = $(this.getAttribute("href"))
        if (target.length) {
            event.preventDefault()
            $("html, body")
                .stop()
                .animate(
                    {
                        scrollTop: target.offset().top - 100,
                    },
                    1000,
                )
        }
    })

    // Back to top button
    var backToTopButton = $(
        '<button class="btn btn-primary btn-floating position-fixed" id="backToTop" style="bottom: 20px; right: 20px; display: none; z-index: 1000;"><i class="bi bi-arrow-up"></i></button>',
    )
    $("body").append(backToTopButton)

    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $("#backToTop").fadeIn()
        } else {
            $("#backToTop").fadeOut()
        }
    })

    $("#backToTop").click(() => {
        $("html, body").animate({ scrollTop: 0 }, 600)
        return false
    })

    // Loading overlay for AJAX requests
    $(document)
        .ajaxStart(() => {
            $("body").append(
                '<div id="loadingOverlay" class="position-fixed w-100 h-100 d-flex justify-content-center align-items-center" style="top: 0; left: 0; background: rgba(0,0,0,0.5); z-index: 9999;"><div class="spinner-border text-light" role="status"><span class="visually-hidden">Loading...</span></div></div>',
            )
        })
        .ajaxStop(() => {
            $("#loadingOverlay").remove()
        })

    // Enhanced form validation
    $(".needs-validation").on("submit", function (event) {
        if (this.checkValidity() === false) {
            event.preventDefault()
            event.stopPropagation()
        }
        $(this).addClass("was-validated")
    })

    // Auto-save draft functionality (for news articles)
    if ($("#NewsContent").length) {
        var autoSaveTimer
        $("#NewsContent, #NewsTitle, #Headline").on("input", () => {
            clearTimeout(autoSaveTimer)
            autoSaveTimer = setTimeout(() => {
                // Auto-save logic here
                console.log("Auto-saving draft...")
            }, 30000) // Save every 30 seconds
        })
    }

    // Print functionality
    window.printReport = () => {
        window.print()
    }

    // Export functionality
    window.exportToCSV = (tableId, filename) => {
        var csv = []
        var rows = document.querySelectorAll("#" + tableId + " tr")

        for (var i = 0; i < rows.length; i++) {
            var row = [],
                cols = rows[i].querySelectorAll("td, th")

            for (var j = 0; j < cols.length; j++) {
                row.push(cols[j].innerText)
            }

            csv.push(row.join(","))
        }

        var csvFile = new Blob([csv.join("\n")], { type: "text/csv" })
        var downloadLink = document.createElement("a")
        downloadLink.download = filename || "export.csv"
        downloadLink.href = window.URL.createObjectURL(csvFile)
        downloadLink.style.display = "none"
        document.body.appendChild(downloadLink)
        downloadLink.click()
        document.body.removeChild(downloadLink)
    }

    // Theme toggle (if implementing dark mode)
    window.toggleTheme = () => {
        $("body").toggleClass("dark-theme")
        localStorage.setItem("theme", $("body").hasClass("dark-theme") ? "dark" : "light")
    }

    // Load saved theme
    if (localStorage.getItem("theme") === "dark") {
        $("body").addClass("dark-theme")
    }

    // Enhanced search with debouncing
    var searchTimer
    $(".search-input").on("input", function () {
        var searchTerm = $(this).val()
        var form = $(this).closest("form")

        clearTimeout(searchTimer)
        searchTimer = setTimeout(() => {
            if (searchTerm.length >= 3) {
                // Perform search
                form.submit()
            }
        }, 500)
    })

    // Keyboard shortcuts
    $(document).keydown((e) => {
        // Ctrl+S to save (prevent default browser save)
        if (e.ctrlKey && e.keyCode === 83) {
            e.preventDefault()
            var saveButton = $('button[type="submit"]:visible').first()
            if (saveButton.length) {
                saveButton.click()
            }
        }

        // Escape to close modals
        if (e.keyCode === 27) {
            $(".modal.show").modal("hide")
        }
    })

    // Initialize rich text editor (if Summernote is included)
    if (typeof $.fn.summernote !== "undefined") {
        $("#NewsContent").summernote({
            height: 300,
            toolbar: [
                ["style", ["style"]],
                ["font", ["bold", "underline", "clear"]],
                ["color", ["color"]],
                ["para", ["ul", "ol", "paragraph"]],
                ["table", ["table"]],
                ["insert", ["link", "picture", "video"]],
                ["view", ["fullscreen", "codeview", "help"]],
            ],
        })
    }
})

// Utility functions
window.FUNews = {
    // Format date
    formatDate: (date) => new Date(date).toLocaleDateString("en-GB"),

    // Format currency
    formatCurrency: (amount) =>
        new Intl.NumberFormat("vi-VN", {
            style: "currency",
            currency: "VND",
        }).format(amount),

    // Show notification
    showNotification: (message, type = "info") => {
        var alertClass = "alert-" + type
        var alert = $(
            '<div class="alert ' +
            alertClass +
            ' alert-dismissible fade show position-fixed" style="top: 20px; right: 20px; z-index: 9999;" role="alert">' +
            message +
            '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
            "</div>",
        )

        $("body").append(alert)

        setTimeout(() => {
            alert.fadeOut(function () {
                $(this).remove()
            })
        }, 5000)
    },

    // Confirm action
    confirm: (message, callback) => {
        if (confirm(message)) {
            callback()
        }
    },
}
