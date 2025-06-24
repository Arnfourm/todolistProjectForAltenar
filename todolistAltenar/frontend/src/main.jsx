import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './styles/general.css'
import Header from './compontents/header.jsx'
import MainContainer from './compontents/mainContainer.jsx'
import Footer from './compontents/footer.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <>
      <Header />
      <MainContainer />
      <Footer />
    </>
  </StrictMode>,
);