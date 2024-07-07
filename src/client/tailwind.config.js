/** @type {import('tailwindcss').Config} */

const defaultTheme = require("tailwindcss/defaultTheme");

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        ...defaultTheme.colors,
        "login-orange": {
          100: "#EB3C01",
        },
      },
    },
  },
  plugins: [],
};
