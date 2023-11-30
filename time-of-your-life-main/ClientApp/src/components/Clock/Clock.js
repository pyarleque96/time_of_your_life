import { useState, useEffect } from 'react'
import './Clock.css'

function Clock(clockProps) {
    const [serverTime, setServerTime] = useState(null);
    const [isNumericMode, setIsNumericMode] = useState(true);

    const getServerTime = async () => {
        try {
            const response = await fetch('system/server-time');
            const serverTime = await response.json();
            setServerTime(new Date(serverTime));
        } catch (error) {
            setServerTime(new Date());
            console.error('Error fetching server time:', error);
        }
    };

    useEffect(() => {
        getServerTime();
    }, []);

    useEffect(() => {
        const localTimerId = setInterval(() => {
            setServerTime((prevServerTime) => {
                return prevServerTime ? new Date(prevServerTime.getTime() + 1000) : null;
            });
        }, 1000);

        return function cleanup() {
            clearInterval(localTimerId);
        };
    }, []);

    const convertTimeToWords = (timeString) => {
        const timeParts = timeString.split(':');
        const hours = parseInt(timeParts[0]);
        const minutes = parseInt(timeParts[1]);

        const numberWords = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 'ten', 'eleven', 'twelve'];
        const minuteWords = ['oh', 'ten', 'twenty', 'thirty', 'forty', 'fifty'];

        let ampm = 'a.m.';
        if (hours >= 12) {
            ampm = 'p.m.';
        }

        const hourWord = numberWords[hours % 12] || 'twelve';
        const minuteWord = minuteWords[Math.floor(minutes / 10)] || '';
        const minuteUnitWord = numberWords[minutes % 10] || '';

        const result = `${hourWord} ${minuteWord} ${minuteUnitWord} ${ampm}`;
        return result.trim();
    };

    const handlePrintButtonClick = () => {
        setIsNumericMode((prevMode) => !prevMode);
    };

    const renderTime = () => {
        if (isNumericMode) {
            return serverTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });
        } else {
            return convertTimeToWords(serverTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }));
        }
    };

    const displayText = serverTime ? renderTime() : 'Loading...';
    
    let displayStyle = {
        fontFamily: clockProps.clockProps.fontFamily
    }

    let titleStyle = {
        fontSize: `${clockProps.clockProps.titleFontSize}pt`,
        color: clockProps.clockProps.titleFontColor,
    }

    let clockStyle = {
        fontSize: `${clockProps.clockProps.clockFontSize}pt`,
        color: clockProps.clockProps.clockFontColor,
    }

    return (
        <div id="Clock">
            <div id="Digits" style={displayStyle}>
                <div id="title" style={titleStyle}>
                    {clockProps.clockProps.titleText}
                </div>
                <div id="time" style={clockStyle}>
                    {displayText}
                </div>
                <button onClick={handlePrintButtonClick}>{isNumericMode ? 'Time in words' : 'Time in numbers'}</button>
            </div>
        </div>
    );
}

export default Clock
