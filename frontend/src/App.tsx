import React, {useState} from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import './Spammer.tsx'
import Spammer from "./Spammer.tsx";

function App() {
    
    const [slidoId, setSlidoId] = useState<string>('')
    const [slidoIdSpammer, setSlidoIdSpammer] = useState<string>('')

    const onSlidoIdBtnClick = function () {
        if (slidoIdSpammer) {
            setSlidoIdSpammer('')
            return
        }
        setSlidoIdSpammer(slidoId)
    }
    
    const onSlidoIdInputChange = function (event: React.ChangeEvent<HTMLInputElement>) {
        setSlidoId(event.target.value);
    }

    return (
        <>
            <div>
                <a href="https://vitejs.dev" target="_blank">
                    <img src={viteLogo} className="logo" alt="Vite logo"/>
                </a>
                <a href="https://react.dev" target="_blank">
                    <img src={reactLogo} className="logo react" alt="React logo"/>
                </a>
            </div>
            <h1>Vite + React</h1>
            <label>Slido Id: </label>
            <input type="text" value={slidoId} onChange={onSlidoIdInputChange}/>
            <button onClick={onSlidoIdBtnClick}>{slidoIdSpammer ? 'Disconnect' : 'Connect'}</button>

            {slidoIdSpammer && <Spammer slidoGuid={slidoIdSpammer}/>}
        </>
    )
}

export default App
