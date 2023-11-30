import { useState, useEffect } from 'react'
import ClockProps from '../Clock/clockProps'
import './SetClockProps.css'

// General validation function
const validateInput = (value, validationFunctions, errorMessages) => {
    for (let i = 0; i < validationFunctions.length; i++) {
        const isValid = validationFunctions[i](value);
        if (!isValid) {
            return { valid: false, error: errorMessages[i] };
        }
    }

    return { valid: true, error: '' };
};

// Specific validation functions
const validateTextLength = (value, maxLength) => value.length <= maxLength;
const validateNotEmpty = (value) => value.trim() !== '';

function SetClockProps({ clockProps, setClockProps }) {
    const clock = new ClockProps()
    const [titleText, setTitleText] = useState(clock.titleText)
    const [fontFamily, setFontFamily] = useState(clock.fontFamily)
    const [titleFontSize, setTitleFontSize] = useState(clock.titleFontSize);
    const [clockFontSize, setClockFontSize] = useState(clock.clockFontSize);
    const [titleFontColor, setTitleFontColor] = useState(clock.titleFontColor)
    const [clockFontColor, setClockFontColor] = useState(clock.clockFontColor)
    const [blinkColons, setBlinkColons] = useState(clock.blinkColons)
    const [presets, setPresets] = useState([])
    const [reloadPresets, setReloadPresets] = useState(false);

    const [loading, setLoading] = useState(true)
    const [isPanelExpanded, setIsPanelExpanded] = useState(true);

    // Validation States
    const [titleTextValid, setTitleTextValid] = useState(true);
    const [titleTextError, setTitleTextError] = useState('');

    const [fontFamilyValid, setFontFamilyValid] = useState(true);
    const [fontFamilyError, setFontFamilyError] = useState('');

    const [titleFontColorValid, setTitleFontColorValid] = useState(true);
    const [titleFontColorError, setTitleFontColorError] = useState('');

    const [clockFontColorValid, setClockFontColorValid] = useState(true);
    const [clockFontColorError, setClockFontColorError] = useState('');

    useEffect(() => {
        ; (async () => {
            const response = await fetch('clock/presets')
            const data = await response.json()
            console.log(data);
            setPresets(data)
            setLoading(false)
        })()
    }, [reloadPresets])

    const sendClockProps = (partialProps) => {
        setClockProps((prevProps) => ({
            ...prevProps,
            ...partialProps,
        }));
    };

    const togglePanel = () => {
        setIsPanelExpanded(!isPanelExpanded);
    };

    const setTitleTextUI = (e) => {
        let titleTextValue = e.target.value

        setTitleText(titleTextValue);
    }

    const setFontFamilyUI = (e) => {
        let fontFamilyValue = e.target.value

        setFontFamily(fontFamilyValue)
    }

    const setTitleFontColurUI = (e) => {
        let fontColorValue = e.target.value

        setTitleFontColor(fontColorValue)
        sendClockProps({ titleFontColor: fontColorValue })
    }

    const setClockFontColurUI = (e) => {
        let fontColorValue = e.target.value

        setClockFontColor(fontColorValue)
        sendClockProps({ clockFontColor: fontColorValue })
    }

    const setBlinkColonsUI = (e) => {
        let blinkColonsValue = e.target.checked

        setBlinkColons(blinkColonsValue)
        sendClockProps({ blinkColons: blinkColonsValue })
    }

    const selectPreset = (preset) => {
        // Actualiza los estados con los valores del preset seleccionado
        setTitleText(preset.titleText);
        setFontFamily(preset.fontFamily);
        setTitleFontSize(preset.titleFontSize);
        setClockFontSize(preset.clockFontSize);
        setTitleFontColor(preset.titleFontColor);
        setClockFontColor(preset.clockFontColor);
        setBlinkColons(preset.blinkColons);

        // Actualiza también el estado global usando la función setClockProps
        setClockProps(preset);
    };

    // Validations
    const handleTitleTextKeyDown = (event) => {
        if (event.key === 'Enter') {
            validateTitleText(event.target.value);
        }
    };

    const validateTitleText = (value) => {

        const validationResult = validateInput(
            value,
            [
                (val) => validateTextLength(val, 30),
                (val) => validateNotEmpty(val)
            ],
            [
                'The Title Text field must be less than 30 characters.',
                'The Title Text field cannot be empty.',
            ]
        );

        setTitleTextValid(validationResult.valid);
        setTitleTextError(validationResult.error);

        if (validationResult.valid) {
            setTitleText(value)
            sendClockProps({ titleText: value })
        }
    };


    const handleFontFamilyKeyDown = (event) => {
        if (event.key === 'Enter') {
            validateFontFamily(event.target.value)
        }
    };

    const validateFontFamily = (value) => {

        const validationResult = validateInput(
            value,
            [
                (val) => validateNotEmpty(val)
            ],
            [
                'The Font Family field cannot be empty.',
            ]
        );

        setFontFamilyValid(validationResult.valid);
        setFontFamilyError(validationResult.error);

        if (validationResult.valid) {
            setFontFamily(value)
            sendClockProps({ fontFamily: value })
        }
    };

    const handleTitleFontColorKeyDown = (event) => {
        if (event.key === 'Enter') {
            validateTitleFontColor(event.target.value)
        }
    };

    const validateTitleFontColor = (value) => {

        const validationResult = validateInput(
            value,
            [
                (val) => validateNotEmpty(val)
            ],
            [
                'The Title Font Color field cannot be empty.',
            ]
        )

        setTitleFontColorValid(validationResult.valid);
        setTitleFontColorError(validationResult.error);

        if (validationResult.valid) {
            setTitleFontColor(value)
            sendClockProps({ titleFontColor: value })
        }
    };

    const handleClockFontColorKeyDown = (event) => {
        if (event.key === 'Enter') {
            validateClockFontColor(event.target.value)
        }
    };

    const validateClockFontColor = (value) => {
        const validationResult = validateInput(
            value,
            [
                (val) => validateNotEmpty(val)
            ],
            [
                'The Clock Font Color field cannot be empty.',
            ]
        )

        setClockFontColorValid(validationResult.valid);
        setClockFontColorError(validationResult.error);

        if (validationResult.valid) {
            setClockFontColor(value)
            sendClockProps({ clockFontColor: value })
        }
    };

    const handleTitleFontSizeChange = (event) => {
        const newSize = parseInt(event.target.value)
        setTitleFontSize(newSize)
        sendClockProps({ titleFontSize: newSize })
    };

    const handleClockFontSizeChange = (event) => {
        const newSize = parseInt(event.target.value)
        setClockFontSize(newSize)
        sendClockProps({ clockFontSize: newSize })
    };

    // Presets
    const presetsDisplay = (() => {
        //console.log(presets)
        return loading ? (
            <div>
                This is a good place to display and use the presets stored on the sever.
            </div>
        ) : (
            <ul>
                {presets.result.presets.map((p, i) => (
                    <li key={p.id}>
                        <button
                            className="preset-button"
                            title="load preset"
                            onClick={() => selectPreset(p)}>
                            Preset {i + 1}:{' '}
                        </button>
                        {`Title Text: ${p.titleText}, Font: ${p.fontFamily}, Title Color: ${p.titleFontColor}, Clock Color: ${p.clockFontColor}, Title Size: ${p.titleFontSize}, Clock Size: ${p.clockFontSize}`}
                    </li>
                ))}
            </ul>
        )
    })()

    const savePresetToServer = async () => {
        try {
            const response = await fetch(clockProps.id ? `clock/presets/${clockProps.id}` : 'clock/presets', {
                method: clockProps.id ? 'PUT' : 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(clockProps),
            });

            if (response.ok) {
                setReloadPresets((prev) => !prev);
                console.log('Preset saved successfully!');
            } else {
                console.error('Failed to save preset:', response.statusText);
                alert('An error occurred while saving the preset. Please try again.');
            }
        } catch (error) {
            console.error('Error while saving preset:', error)
            alert('An error occurred while saving the preset. Please try again.')
        }
    };


    return (
        <div id="ClockProps" className={isPanelExpanded ? 'expanded' : 'collapsed'}>
            <div
                style={{
                    float: 'left',
                    width: '40px',
                    border: '1px solid white',
                    fontSize: '20pt',
                }}
            >
                <a
                    style={{ cursor: 'pointer' }}
                    onClick={togglePanel}
                    title={isPanelExpanded ? 'collapse' : 'expand'}
                >
                    {isPanelExpanded ? '-' : '+'}
                </a>
            </div>
            <div>
                <div>
                    <h1>Clock Properties</h1>
                    <hr />
                </div>
                <div>
                    <div>
                        <h2>Settings</h2>
                    </div>
                    <div>
                        <div>Title Text</div>
                        <div>
                            <input
                                id="titleText"
                                value={titleText}
                                onChange={setTitleTextUI}
                                onKeyDown={handleTitleTextKeyDown}
                                className={titleTextValid ? 'valid-input' : 'invalid-input'}
                            />
                            <button onClick={validateTitleText}>✓</button>
                        </div>
                        {titleTextError && <div className="error-message">{titleTextError}</div>}
                    </div>
                    <div>
                        <div>Font Family</div>
                        <div>
                            <input
                                id="fontFamily"
                                value={fontFamily}
                                onChange={setFontFamilyUI}
                                onKeyDown={handleFontFamilyKeyDown}
                                className={fontFamilyValid ? 'valid-input' : 'invalid-input'}
                            />
                            <button onClick={validateFontFamily}>✓</button>
                        </div>
                        {fontFamilyError && <div className="error-message">{fontFamilyError}</div>}
                    </div>
                    <div>
                        <div>Title Font Size</div>
                        <div>
                            <input
                                type="range"
                                id="titleFontSize"
                                min="12"
                                max="72"
                                value={titleFontSize}
                                onChange={handleTitleFontSizeChange}
                            />
                            <span>{titleFontSize}pt</span>
                        </div>
                    </div>
                    <div>
                        <div>Clock Font Size</div>
                        <div>
                            <input
                                type="range"
                                id="clockFontSize"
                                min="12"
                                max="72"
                                value={clockFontSize}
                                onChange={handleClockFontSizeChange}
                            />
                            <span>{clockFontSize}pt</span>
                        </div>
                    </div>
                    <div>
                        <div>Title Font Color</div>
                        <div>
                            <input
                                id="titleFontColor"
                                type="color"
                                value={titleFontColor}
                                onChange={(e) => setTitleFontColurUI(e)}
                                className={titleFontColorValid ? 'valid-input' : 'invalid-input'}
                            />
                        </div>
                        {titleFontColorError && <div className="error-message">{titleFontColorError}</div>}
                    </div>
                    <div>
                        <div>Clock Font Color</div>
                        <div>
                            <input
                                id="clockFontColor"
                                type="color"
                                value={clockFontColor}
                                onChange={(e) => setClockFontColurUI(e)}
                                className={clockFontColorValid ? 'valid-input' : 'invalid-input'}
                            />
                        </div>
                        {clockFontColorError && <div className="error-message">{clockFontColorError}</div>}
                    </div>
                    <div>
                        <div>Blink Colons</div>
                        <div>
                            <input
                                id="blinkColons"
                                checked={blinkColons}
                                type="checkbox"
                                onChange={setBlinkColonsUI}
                            />
                        </div>
                    </div>
                    <div>
                        <div>
                            <button
                                onClick={savePresetToServer}
                            >
                                Save Preset
                            </button>
                        </div>
                    </div>
                </div>
                <hr />
                <div>
                    <h2>Presets</h2>
                    <div>{presetsDisplay}</div>
                </div>
            </div>
        </div>
    )
}

export default SetClockProps
