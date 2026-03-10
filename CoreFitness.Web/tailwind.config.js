/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml",
    "./Pages/**/*.cshtml",
    "./wwwroot/**/*.html"
  ],
  theme: {
    extend: {
      colors: {
        'gym-lime': '#d3f54a', // Din snygga färg från Figma!
      }
    },
  },
  plugins: [],
}