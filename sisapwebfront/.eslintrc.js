module.exports = {
  root: true,
  env: {
    node: true
  },
  'extends': [
    'plugin:vue/essential',
    'eslint:recommended',
    '@vue/typescript/recommended',
  ],
  parserOptions: {
    parser: '@typescript-eslint/parser',
    ecmaVersion: 2020
  },
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'quotes': ['error', 'single'],
    'array-bracket-spacing': 0,
    'object-curly-spacing': 0,
    'block-spacing': 0,
    'indent': ["error", 2],
    'semi': ['error', 'always'],
    'space-in-parens': ['error', 'never'],
    'no-use-before-define': ['error', { 'variables': false }],
    'no-unused-vars': 0,
    '@typescript-eslint/no-unused-vars': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    '@typescript-eslint/no-inferrable-types': 'off'
  }
}
