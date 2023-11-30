import { useState } from 'react'
import Clock from './components/Clock/Clock.js'
import ClockProps from './components/Clock/clockProps.js'
import SetClockProps from './components/SetClockProps/SetClockProps.js'
import './styles/App.css'

function App() {
    const [clockProps, setClockProps] = useState(new ClockProps())

    return (
        <div className="App">
            <Clock clockProps={clockProps} />
            <SetClockProps clockProps={clockProps} setClockProps={setClockProps} />
        </div>
    )
}

export default App
