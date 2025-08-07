module.exports = {
  content: ["./**/*.razor", "./wwwroot/**/*.html", "./wwwroot/**/*.js", "./**/*.{razor,cshtml,html}"],
  theme: {
    extend: {
      primary: {
        DEFAULT: '#5e22d3',
        50: '#EFE9FB', // Lighter shade
        100: '#9e7ae5',
        300: '#5e22d3',
        500: '#4b1ba9',
        700: '#2f116a',
        900: '#1c0a3f',
        950: '#090315' //Darkest shade
      },
      secondary: {
        DEFAULT: '#3d2ddd',
        50: '#c5c0f5', 
        100: '#6457e4',
        300: '#3d2ddd',
        500: '#2b1f9b',
        700: '#181258',
        900: '#0c092c',
        950: '#060416' 
      },
      bak: '#170937',
      accent: '#b2d152',
      textcol: '#e1daf5'
    },
  },
  plugins: [],
};
