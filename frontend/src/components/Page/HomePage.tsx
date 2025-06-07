import React from 'react';
import bg from '../../assets/bg.png';
import a1 from '../../assets/a1.png';
import a2 from '../../assets/a2.png';
import a3 from '../../assets/a3.png';
import a4 from "../../assets/a4.png";
import a5 from "../../assets/a5.png";

const HomePage: React.FC = () => {
    return (
        <div className='bg-cover relative' style={{ backgroundImage: `url(${bg})` }}>
            <nav className='flex items-center justify-between bg-indigo-950 p-4'>
                <div className='ml-10 text-white justify-start text-4xl font-bold shadow-md'>AIDIMS</div>
                <div className='flex space-x-20 absolute left-1/2 transform -translate-x-1/2 text-2xl'>
                    <a href="#problem" className='text-white hover:text-gray-300'>PROBLEM </a>
                    <a href="#solution" className='text-white hover:text-gray-300'>SOLUTION</a>
                </div>
                <div className='mr-10 text-2xl'>
                    <button className='bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition-colors'>
                        LOGIN
                    </button>
                </div>
            </nav >
            <div className='items-center justify-center text-white' >
                <section className="px-12 pt-12 ">
                    <div className='flex flex-col items-center justify-center'>
                        <h1 className="text-5xl font-bold mb-6">AIDIMS</h1>
                        <h2 className="text-3xl font-medium text-cyan-300 ">AI DICOM Image Management System</h2>
                    </div>
                    <div className='flex flex-col lg:flex-row lg:items-center top-2'>
                        <div className="lg:w-2/3">
                            <p className="text-xl leading-relaxed">
                                AIDIMS automates the import, storage, and organization of medical images in DICOM format,
                                such as X-rays, CT scans, and MRIs. The system integrates AI to analyze and support fast,
                                accurate disease diagnosis.
                            </p>
                            <ul className="list-disc list-inside mt-4 space-y-1 text-xl text-white">
                                <li>Managing images by patient, device, date, and physician.</li>
                                <li>AI-powered disease detection and classification.</li>
                                <li>Patient record and imaging history management.</li>
                                <li>Image annotation, comparison, and report generation.</li>
                                <li>Notifications for AI results and priority cases.</li>
                                <li>Secure, high performance, and easy to use for all hospital users.</li>
                            </ul>
                        </div>
                        <div className='lg:w-1/3 flex lg:items-center justify-center'>
                            <img src={a1} alt="AI Human" className="w-full h-auto object-contain" />
                        </div>
                    </div>
                </section>
                <section className="px-12 ">
                    <div className='flex items-center justify-center'>
                        <h1 className='text-3xl text-black mb-12'>PROBLEM</h1>
                    </div>
                    <div className='flex flex-col lg:flex-row lg:items-center gap-10 top-2'>
                        <div className="lg:w-1/3">
                            <img src={a2} alt="Problem Image" className="w-60 h-auto object-cover mb-4 lg:mb-0" />
                        </div>
                        <div className="lg:w-2/3">
                            <p className="text-xl leading-relaxed">
                                Hospitals struggle with managing and analyzing large volumes of medical images in DICOM format. Manual handling leads to delays, data loss, and reduced diagnostic accuracy — directly affecting patient care and treatment outcomes.
                            </p>
                        </div>
                    </div>
                </section>
                <section className="px-12 ">
                    <div className='flex items-center justify-center'>
                        <h1 className='text-3xl text-black mb-12'>SOLUTION</h1>
                    </div>
                    <div className='flex flex-col lg:flex-row lg:items-center gap-10 top-2'>
                        <div className="lg:w-2/3">
                        <p className="text-xl leading-relaxed">
                            AIDIMS provides an AI-powered platform that automates DICOM image management, links imaging data with patient records, and supports physicians with intelligent diagnostic suggestions — ensuring faster, safer, and more accurate healthcare delivery
                        </p>
                        </div>
                        <div className="lg:w-1/3">
                            <img src={a3} alt="Solution Image" className="w-full h-auto rounded-r-full object-cover mb-4 lg:mr-0" />
                        </div>
                    </div>
                </section>
            </div>
            <footer className=" text-white py-10 px-12 gap-8" style={{ backgroundImage: `url(${a5})`, backgroundSize: 'cover' }}>
            <div className="flex flex-col lg:flex-row lg:items-center gap-10 top-2">
                <div className="lg:w-1/3 items-start">
                    <img src={a4} alt="Footer Image" className="w-full h-auto object-cover mb-4 lg:mb-0" />
                </div>
                <div className="lg:w-2/3 text-center">
                    <div className="grid grid-cols-3 gap-6">
                        <div>
                            <h2 className="text-2xl font-bold mb-4">Company</h2>
                            <ul className="list-none space-y-2">
                                <li>About</li>
                                <li>News</li>
                                <li>Blog</li>
                                <li>FAQ</li>
                                <li>Plans</li>
                                <li>Privacy Policy</li>
                                <li>Terms of Service</li>
                                <a href="https://youtube.com" target="_blank" rel="noopener noreferrer">Youtube</a> <br />
                                <a href="https://facebook.com" target="_blank" rel="noopener noreferrer">Facebook</a> <br />
                                <a href="contact">Contact</a>
                            </ul>
                        </div>
                        <div className="text-center">
                            <h2 className="text-2xl font-bold mb-4">Image Tools</h2>
                            <ul className="list-none space-y-2">
                                <li>Import DICOM Images</li>
                                <li>AI-Based Image Classification</li>
                                <li>Compare Images Over Time</li>
                                <li>Image Annotation Support</li>
                                <li>View & Adjust Images (Zoom, Pan, Contrast)</li>
                            </ul>       
                            </div>
                        <div className="text-center break-words">
                            <h2 className="text-2xl font-bold mb-4">AI & Diagnostic Features</h2>
                            <ul className="list-none space-y-2">
                                <li>Auto Disease Detection from Images</li>
                                <li>Receive AI Diagnostic Suggestions</li>
                                <li>Prioritized Case Notification</li>
                                <li>Physician Review Reminder</li>
                                <li>Diagnostic Accuracy Statistics</li>
                                <li>Strong AI Model Integration</li>
                                <li>Compare AI Results with Past Data</li>
                                <li>Annotate & Note on AI Outputs</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
        </div>
        


    )
};
export default HomePage;
