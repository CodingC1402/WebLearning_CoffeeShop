import React from 'react'
import { usePageName } from '../utils/extraHook';

type HomeProps = {
  name: string;
}

const Home = (props : HomeProps) => {
  usePageName(props.name);

  return (
    <div style={{height: '100%', backgroundColor: 'black'}}>Home page for react</div>
  )
}

Home.propTypes = {}

export default Home