

const validateField = (field) => {

    console.log("Validering körs för: " + field.name);

    let errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`)
    if (!errorSpan) return;

    let errorMessage = ""
    let value = field.value.trim()

    if (field.hasAttribute("data-val-required") && value === "") {
        errorMessage = field.getAttribute("data-val-required")
    }

    if (field.hasAttribute("data-val-regex") && value !== "") {
        let pattern = new RegExp(field.getAttribute("data-val-regex-pattern"))
        if (!pattern.test(value)) {
            errorMessage = field.getAttribute("data-val-regex")
        }
    }

    if (errorMessage) {
        field.classList.add("input-validation-error")
        errorSpan.classList.remove("field-validation-valid")
        errorSpan.classList.add("field-validation-error")
        errorSpan.textContent = errorMessage
    } else {
        field.classList.remove("input-validation-error")
        errorSpan.classList.remove("field-validation-error")
        errorSpan.classList.add("field-validation-valid")
        errorSpan.textContent = ""
    }
}


document.addEventListener("DOMContentLoaded", function () {
    // 1. Hitta ALLA formulär på sidan (istället för bara det första)
    const forms = document.querySelectorAll("form");

    // 2. Loopa igenom varje formulär
    forms.forEach(form => {
        // 3. Hitta alla fält med validering INUTI just detta formulär
        const fields = form.querySelectorAll("input[data-val='true']");

        fields.forEach(field => {
            // 4. Koppla ihop valideringsfunktionen
            field.addEventListener("input", function () {
                validateField(field);

            });
        });
    });
});