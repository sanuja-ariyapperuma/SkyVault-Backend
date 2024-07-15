/** @type {import('tailwindcss').Config} */

const defaultTheme = require("tailwindcss/defaultTheme");

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        ...defaultTheme.colors,
        "login-orange": "#EB3C01",
        "button-primary": "#08577E",
        "button-primary-hover": "#063e59",
      },
    },
  },
  plugins: [],
};
